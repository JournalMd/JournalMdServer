using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Tags;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : BaseCRUDController<TagsService, TagInput, TagOutput>
    {
        public TagsController(TagsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
