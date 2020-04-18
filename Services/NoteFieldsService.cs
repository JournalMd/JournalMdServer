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

        // TODO do we need this right now - NodeTypes will include fields anyway...

        // Intentionally left empty
    }
}
