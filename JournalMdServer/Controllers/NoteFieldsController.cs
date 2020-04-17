using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.NoteFields;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteFieldsController : BaseRController<NoteFieldsService, NoteFieldOutput>
    {
        public NoteFieldsController(NoteFieldsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
