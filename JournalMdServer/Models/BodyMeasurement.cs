using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class BodyMeasurement : BaseAuditEntity, IBaseModel
    {
        public string Description { get; set; }

        public double Chest { get; set; } // Brustumfang

        public double Waist { get; set; } // Taillenumfang

        public double Hips { get; set; } // Hüftumfang

        public double Arm { get; set; } // Oberarm/Biceps

        public double Leg { get; set; } // Oberschenkel

        public double Calf { get; set; } // Waden

        public double BodyFatMass { get; set; } // Körperfettanteil

        public double BodyFatPercentage { get; set; } // Body Fat %

        public double TotalBodyWater { get; set; } // Wasseranteil

        public double MuscleMass { get; set; } // Muskelanteil

        // TODO Images
    }
}
