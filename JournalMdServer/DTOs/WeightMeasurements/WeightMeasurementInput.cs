using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.WeightMeasurements
{
    public class WeightMeasurementInput
    {
        public string Description { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Height { get; set; }

        public double GoalWeight { get; set; }
    }
}
