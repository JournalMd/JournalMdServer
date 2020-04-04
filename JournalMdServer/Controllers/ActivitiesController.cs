using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Activities;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseCRUDController<ActivitiesService, ActivityInput, ActivityOutput>
    {
        public ActivitiesController(ActivitiesService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
