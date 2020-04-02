using System.Collections.Generic;
using System.Threading.Tasks;
using JournalMdServer.Repositories;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using JournalMdServer.DTOs.Notes;
using AutoMapper;
using JournalMdServer.Helpers;
using System.Web.Http.ModelBinding;

namespace JournalMdServer.Services
{
    public class NotesService
    {
        private readonly IRepository<Note> _repository;
        private readonly IMapper _mapper;

        public NotesService(IRepository<Note> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteOutput>> GetItems(long userId)
        {
            var notes = await _repository.Query.Where(m => m.UserId == userId).ToListAsync();
            return _mapper.Map<List<NoteOutput>>(notes);
            //var notes = _repository.Query.GetPaged(1, 3);
            //return _mapper.Map<PagedResult<NoteOutput>>(notes);
        }

        public async Task<NoteOutput> GetItem(long id, long userId)
        {
            var note = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);
            return _mapper.Map<NoteOutput>(note);
        }

        public async Task<NoteOutput> Insert(NoteInput note, long userId)
        {
            var entry = _mapper.Map<Note>(note);
            entry.UserId = userId;
            _repository.Insert(entry);
            await _repository.Context.SaveChangesAsync();
            return _mapper.Map<NoteOutput>(entry);
        }

        public async Task Update(long id, NoteInput note, long userId)
        {
            var dbNote = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (id != dbNote.Id)
                throw new ArgumentException("Invalid id");

            dbNote = _mapper.Map<NoteInput, Note>(note, dbNote);
            _repository.Update(dbNote);
            await _repository.Context.SaveChangesAsync();
        }

        public async Task<long?> Delete(long id, long userId)
        {
            var note = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (note == null)
                return null;

            _repository.Delete(note);
            await _repository.Context.SaveChangesAsync();

            return id;
        }

        /*private bool ItemExists(long id, long userId)
        {
            return _repository.Query.Any(m => m.UserId == userId && m.Id == id);
        }*/
    }
}
