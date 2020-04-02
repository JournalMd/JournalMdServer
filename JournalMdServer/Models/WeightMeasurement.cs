using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class WeightMeasurement : BaseEntity, IBaseModel
    {
        [Required]
        public double Weight { get; set; }
        
        [Required]
        public double Height { get; set; }

        public string Description { get; set; }

        // FK
        public long UserId { get; set; }
        public User User{ get; set; }
    }
}
