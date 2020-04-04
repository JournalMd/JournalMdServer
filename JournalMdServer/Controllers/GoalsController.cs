using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Goals;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : BaseCRUDController<GoalsService, GoalInput, GoalOutput>
    {
        public GoalsController(GoalsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
