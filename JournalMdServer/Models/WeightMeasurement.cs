using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class WeightMeasurement : BaseAuditEntity, IBaseModel
    {
        [Required]
        public double Weight { get; set; }
        
        [Required]
        public double Height { get; set; }

        public string Description { get; set; }
    }
}
