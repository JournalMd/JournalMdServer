using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.NoteTypes;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteTypesController : BaseRController<NoteTypesService, NoteTypeOutput>
    {
        public NoteTypesController(NoteTypesService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
