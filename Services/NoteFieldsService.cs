using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.NoteFields;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class NoteFieldsService : BaseRService<NoteField, NoteFieldOutput>
    {
        public NoteFieldsService(IRepository<NoteField> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
