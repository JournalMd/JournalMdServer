using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.WeightMeasurements;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeightMeasurementsController : BaseCRUDController<WeightMeasurementsService, WeightMeasurementInput, WeightMeasurementOutput>
    {
        public WeightMeasurementsController(WeightMeasurementsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
