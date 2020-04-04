using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.WeightMeasurements
{
    public class WeightMeasurementOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double GoalWeight { get; set; }
    }
}
