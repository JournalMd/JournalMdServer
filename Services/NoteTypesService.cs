using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.NoteTypes;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class NoteTypesService : BaseRService<NoteType, NoteTypeOutput>
    {
        public NoteTypesService(IRepository<NoteType> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
