using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.BodyMeasurements;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class BodyMeasurementsService : BaseCRUDService<BodyMeasurement, BodyMeasurementInput, BodyMeasurementOutput>
    {
        public BodyMeasurementsService(IRepository<BodyMeasurement> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
