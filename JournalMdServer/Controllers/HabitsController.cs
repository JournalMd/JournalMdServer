using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Habits;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : BaseCRUDController<HabitsService, HabitInput, HabitOutput>
    {
        public HabitsController(HabitsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
