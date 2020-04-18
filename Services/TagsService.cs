using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Tags;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class TagsService : BaseCRUDService<Tag, TagInput, TagOutput>
    {
        public TagsService(IRepository<Tag> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
