using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Notes;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseCRUDController<NotesService, NoteInput, NoteOutput>
    {
        public NotesController(NotesService service): base(service)
        {
        }

        // Intentionally left empty
    }
}
