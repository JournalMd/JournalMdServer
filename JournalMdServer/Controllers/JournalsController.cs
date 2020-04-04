using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Journals;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JournalsController : BaseCRUDController<JournalsService, JournalInput, JournalOutput>
    {
        public JournalsController(JournalsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
