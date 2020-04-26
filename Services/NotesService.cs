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

namespace JournalMdServer.Services
{
    public class NotesService: BaseCRUDService<Note, NoteInput, NoteOutput>
    {
        protected readonly IRepository<NoteField> noteFieldRepository;

        public NotesService(IRepository<Note> repository, IRepository<NoteField> nfRepo, IMapper mapper): base(repository, mapper)
        {
            noteFieldRepository = nfRepo;
        }

        public override async Task<ActionResult<IEnumerable<NoteOutput>>> GetAll(long userId)
        {
            var entities = await _repository.Query.Include(inc => inc.NoteValues).Where(m => m.UserId == userId).ToListAsync();
            return _mapper.Map<List<NoteOutput>>(entities);
        }

        public override async Task<NoteOutput> Create(NoteInput inputModel, long userId)
        {
            var entry = _mapper.Map<Note>(inputModel);
            entry.SetCreateFields(userId);

            var noteFields = await noteFieldRepository.Query.Where(nf => nf.NoteTypeId == entry.NoteTypeId).ToListAsync();

            foreach (var e in entry.NoteValues)
            {
                // Validate fieldtype-fields to prevent adding data to wrong fields/types
                if(!noteFields.Exists(nf => nf.Id == e.NoteFieldId))
                    throw new AppException("Field not found on type.");

                ValidateField(e, noteFields.First(nf => nf.Id == e.NoteFieldId));

                e.Id = 0; // Prevent that the user can set a real id!
                e.SetCreateFields(userId);
            }

            _repository.Insert(entry);
            await _repository.Context.SaveChangesAsync();

            return _mapper.Map<NoteOutput>(entry);
        }

        public override async Task Update(long id, NoteInput inputModel, long userId)
        {
            var dbEntry = await _repository.Query.Include(inc => inc.NoteValues).FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (dbEntry == null || id != dbEntry.Id)
                throw new AppException("Invalid id");

            dbEntry = _mapper.Map(inputModel, dbEntry);
            dbEntry.SetUpdateFields(userId);

            _repository.Update(dbEntry);
            await _repository.Context.SaveChangesAsync();
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

        // TODO Rules Parser
        // TODO Calculator

        // TODO move this code to calculator
        /*

        /// <summary>	
        /// Calculated	
        /// https://de.wikipedia.org/wiki/Body-Mass-Index	
        /// </summary>	
        public double? BodyMassIndex	
        {	
            get	
            {	
                if (Weight <= 0 || Height <= 0)	
                    return null;	

                return Weight / (Height * Height) * 10000.0;	
            }	
        }	

        /// <summary>	
        /// Calculated	
        /// https://de.wikipedia.org/wiki/Ponderal-Index	
        /// </summary>	
        public double? PonderalIndex	
        {	
            get	
            {	
                if (Weight <= 0 || Height <= 0)	
                    return null;	

                return Weight / (Height * Height * Height) * 1000000.0;	
            }	
        }	
    
        /// <summary>	
        /// Calculated	
        /// https://en.wikipedia.org/wiki/Waist%E2%80%93hip_ratio	
        /// https://de.wikipedia.org/wiki/Taille-H%C3%BCft-Verh%C3%A4ltnis	
        /// </summary>	
        public double? WaistToHipRatio	
        {  	
            get	
            {	
                if (Waist == null || Hips == null || Waist <= 0 || Hips <= 0)	
                    return null;	

                return Waist / Hips;	
            } 	
        }    
        */
    }
}
