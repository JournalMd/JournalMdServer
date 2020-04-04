using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Tasks;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : BaseCRUDController<TasksService, TaskInput, TaskOutput>
    {
        public TasksController(TasksService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
