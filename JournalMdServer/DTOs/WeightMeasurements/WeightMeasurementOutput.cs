using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.WeightMeasurements
{
    public class WeightMeasurementOutput
    {
        public long Id { get; set; }

        public double Weight { get; set; }
    
        public double Height { get; set; }

        public string Description { get; set; }
    }
}
