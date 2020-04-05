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

        public double? GoalWeight { get; set; }

        /// <summary>
        /// Calculated
        /// https://de.wikipedia.org/wiki/Body-Mass-Index
        /// </summary>
        public double? BodyMassIndex
        {
            get
            {
                if (Weight <= 0 || Height <= 0)
                    return null;

                return Weight / (Height * Height) * 10000.0;
            }
        }

        /// <summary>
        /// Calculated
        /// https://de.wikipedia.org/wiki/Ponderal-Index
        /// </summary>
        public double? PonderalIndex
        {
            get
            {
                if (Weight <= 0 || Height <= 0)
                    return null;

                return Weight / (Height * Height * Height) * 1000000.0;
            }
        }
    }
}
