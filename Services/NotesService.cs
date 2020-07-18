using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Notes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System;
using JournalMdServer.Helpers;
using JournalMdServer.Notes;

namespace JournalMdServer.Services
{
    public class NotesService: BaseCRUDService<Note, NoteInput, NoteOutput>
    {
        protected readonly IRepository<NoteField> noteFieldRepository;
        protected readonly IRepository<Category> categoryRepository;
        protected readonly IRepository<Tag> tagRepository;

        public NotesService(IRepository<Note> repository, IRepository<NoteField> nfRepo, IRepository<Category> catRepo, IRepository<Tag> tagRepo, IMapper mapper): base(repository, mapper)
        {
            noteFieldRepository = nfRepo;
            categoryRepository = catRepo;
            tagRepository = tagRepo;
        }

        public override async Task<ActionResult<IEnumerable<NoteOutput>>> GetAll(long userId)
        {
            var entities = await _repository.Query
                .Include(inc => inc.NoteValues)
                .Include(inc => inc.NoteCategories)
                .Include(inc => inc.NoteTags)
                .Where(m => m.UserId == userId)
                .ToListAsync();
            return _mapper.Map<List<NoteOutput>>(entities);
        }

        public override async Task<NoteOutput> Create(NoteInput inputModel, long userId)
        {
            var entry = _mapper.Map<Note>(inputModel);
            entry.SetCreateFields(userId);

            // Validate and add fields
            var noteFields = await noteFieldRepository.Query.Where(nf => nf.NoteTypeId == entry.NoteTypeId).ToListAsync();
            foreach (var nField in noteFields)
            {
                // Validate fieldtype-fields to prevent adding data to wrong fields/types
                var noteValue = entry.NoteValues.SingleOrDefault(nv => nv.NoteFieldId == nField.Id);
                if (noteValue == null)
                    throw new AppException(String.Format("Field '{0}' not found.", nField.Title));

                ValidateField(noteValue, nField);

                noteValue.Id = 0; // Prevent that the user can set a real id!
                noteValue.SetCreateFields(userId);
            }

            // Validate and add category
            if (inputModel.Categories != null)
            {
                var categories = await categoryRepository.Query.Where(cat => inputModel.Categories.Contains(cat.Id)).ToListAsync(); // No owner check here
                if (categories.Count() != inputModel.Categories.Count())
                    throw new AppException("Invalid category assignment.");
                entry.NoteCategories = categories.Select(sel => new NoteCategory() { CategoryId = sel.Id }).ToList();
            }

            // Validate and add tags
            if (inputModel.Tags != null)
            {
                var tags = await tagRepository.Query.Where(tag => tag.UserId == userId && inputModel.Tags.Contains(tag.Id)).ToListAsync(); // Owner check!
                if (tags.Count() != inputModel.Tags.Count())
                    throw new AppException("Invalid tag assignment.");
                entry.NoteTags = tags.Select(sel => new NoteTag() { TagId = sel.Id }).ToList();
            }

            UpdateCalculatedFields(entry, noteFields);

            _repository.Insert(entry);
            await _repository.Context.SaveChangesAsync();

            return _mapper.Map<NoteOutput>(entry);
        }

        public override async Task<NoteOutput> Update(long id, NoteInput inputModel, long userId)
        {
            var dbEntry = await _repository.Query
                .Include(inc => inc.NoteValues)
                .Include(inc => inc.NoteCategories)
                .Include(inc => inc.NoteTags)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (dbEntry == null || id != dbEntry.Id)
                throw new AppException("Invalid id");

            // Update entity
            // dbEntry = _mapper.Map(inputModel, dbEntry); DONT USE AUTOMAPPER - Merging is more complex here!
            // NEVER merge NoteTypeId!
            dbEntry.Title = inputModel.Title;
            dbEntry.Description = inputModel.Description;
            dbEntry.Mood = inputModel.Mood;
            dbEntry.Date = inputModel.Date;
            dbEntry.Title = inputModel.Title;
            dbEntry.SetUpdateFields(userId);

            // Update Note Values (they must exist as the create process already created them! - keep in mind for migration scripts with new fields - they need to create all fields!)
            var noteFields = await noteFieldRepository.Query.Where(nf => nf.NoteTypeId == dbEntry.NoteTypeId).ToListAsync();
            foreach (var nField in noteFields)
            {
                // Validate fieldtype-fields to prevent adding data to wrong fields/types
                var inputNoteValue = inputModel.NoteValues.SingleOrDefault(nv => nv.NoteFieldId == nField.Id);
                if (inputNoteValue == null)
                    throw new AppException(String.Format("Field '{0}' not found.", nField.Title));

                var dbNoteValue = dbEntry.NoteValues.Single(nf => nf.NoteFieldId == inputNoteValue.NoteFieldId);
                dbNoteValue = _mapper.Map(inputNoteValue, dbNoteValue); // NoteFieldId is kept the same as this is used to find the entry
                ValidateField(dbNoteValue, nField);
                dbNoteValue.SetUpdateFields(userId);
            }

            UpdateCalculatedFields(dbEntry, noteFields);

            // Validate and add category
            var categories = await categoryRepository.Query.Where(cat => inputModel.Categories.Contains(cat.Id)).ToListAsync(); // No owner check here
            if (categories.Count() != inputModel.Categories.Count())
                throw new AppException("Invalid category assignment.");
            foreach(var existingCategory in dbEntry.NoteCategories.Where(sel => !categories.Any(subSel => subSel.Id == sel.CategoryId)).ToList()) // Remove all entries not in the list anymore
            {
                dbEntry.NoteCategories.Remove(existingCategory);
            }
            foreach (var newCategory in categories.Where(sel => !dbEntry.NoteCategories.Any(subSel => subSel.CategoryId == sel.Id )).ToList()) // Add not existing ones
            {
                dbEntry.NoteCategories.Add(new NoteCategory() { CategoryId = newCategory.Id });
            }

            // Validate and add tags
            var tags = await tagRepository.Query.Where(tag => tag.UserId == userId && inputModel.Tags.Contains(tag.Id)).ToListAsync(); // Owner check!
            if (tags.Count() != inputModel.Tags.Count())
                throw new AppException("Invalid tag assignment.");
            foreach (var existingTag in dbEntry.NoteTags.Where(sel => !tags.Any(subSel => subSel.Id == sel.TagId)).ToList()) // Remove all entries not in the list anymore
            {
                dbEntry.NoteTags.Remove(existingTag);
            }
            foreach (var newTag in tags.Where(sel => !dbEntry.NoteTags.Any(subSel => subSel.TagId == sel.Id)).ToList()) // Add not existing ones
            {
                dbEntry.NoteTags.Add(new NoteTag() { TagId = newTag.Id });
            }

            _repository.Update(dbEntry);
            await _repository.Context.SaveChangesAsync();
            return _mapper.Map<NoteOutput>(dbEntry);
        }

        private void ValidateField(NoteValue e, NoteField noteField)
        {
            // required
            if (noteField.Required && e.Value == "")
                throw new AppException(string.Format("Field '{0}' is required.", noteField.Title));

            // types
            if (e.Value == "") // return if there is no value to be converted
                return;

            if ((noteField.Type == "datetime" || noteField.Type == "date") && !DateTime.TryParse(e.Value, out _))
                throw new AppException(String.Format("Field '{0}' does not contain a valid date.", noteField.Title));

            if ((noteField.Type == "boolean" || noteField.Type == "bool") && !bool.TryParse(e.Value, out _))
                throw new AppException(String.Format("Field '{0}' does not contain a valid bool.", noteField.Title));

            if (noteField.Type == "number" && !double.TryParse(e.Value, out _))
                throw new AppException(String.Format("Field '{0}' does not contain a valid number.", noteField.Title));
        }

        private void UpdateCalculatedFields(Note note, List<NoteField> noteFields)
        {
            foreach (var nField in noteFields)
            {
                if(nField.Type == "calculated")
                {
                    var calcRule = (new RulesParser(nField.Rules)).GetValue("calculation");
                    var noteValue = note.NoteValues.Single(nf => nf.NoteFieldId == nField.Id);
                    noteValue.Value = Calculator.CalculateField(calcRule, note, noteFields);
                }
            }            
        }
    }
}
