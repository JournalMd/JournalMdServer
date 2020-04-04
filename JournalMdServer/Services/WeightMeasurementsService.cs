using System.Collections.Generic;
using System.Threading.Tasks;
using JournalMdServer.Repositories;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using JournalMdServer.DTOs.WeightMeasurements;
using AutoMapper;
using JournalMdServer.Helpers;
using System.Web.Http.ModelBinding;

namespace JournalMdServer.Services
{
    public class WeightMeasurementsService : BaseCRUDService<WeightMeasurement, WeightMeasurementInput, WeightMeasurementOutput>
    {
        public WeightMeasurementsService(IRepository<WeightMeasurement> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
