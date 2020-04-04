using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Routines;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : BaseCRUDController<RoutinesService, RoutineInput, RoutineOutput>
    {
        public RoutinesController(RoutinesService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
