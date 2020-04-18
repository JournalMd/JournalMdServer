using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Notes;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class NotesService: BaseCRUDService<Note, NoteInput, NoteOutput>
    {
        public NotesService(IRepository<Note> repository, IMapper mapper): base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
