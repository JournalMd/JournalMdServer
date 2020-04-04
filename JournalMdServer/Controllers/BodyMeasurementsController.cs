using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.BodyMeasurements;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BodyMeasurementsController : BaseCRUDController<BodyMeasurementsService, BodyMeasurementInput, BodyMeasurementOutput>
    {
        public BodyMeasurementsController(BodyMeasurementsService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
