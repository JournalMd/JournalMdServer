using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.BodyMeasurements
{
    public class BodyMeasurementOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public double Chest { get; set; }

        public double Waist { get; set; }

        public double Hips { get; set; }

        public double Arm { get; set; }

        public double Leg { get; set; }

        public double Calf { get; set; }

        public double BodyFatMass { get; set; }

        public double BodyFatPercentage { get; set; }

        public double TotalBodyWater { get; set; }

        public double MuscleMass { get; set; }

        /// <summary>
        /// Calculated
        /// https://en.wikipedia.org/wiki/Waist%E2%80%93hip_ratio
        /// https://de.wikipedia.org/wiki/Taille-H%C3%BCft-Verh%C3%A4ltnis
        /// </summary>
        public double? WaistToHipRatio
        {  
            get
            {
                if (/*Waist == null || Hips == null ||*/ Waist <= 0 || Hips <= 0)
                    return null;

                return Waist / Hips;
            } 
        }
    }
}
