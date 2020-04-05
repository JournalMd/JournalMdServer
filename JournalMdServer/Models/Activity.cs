using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Activity : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int Mood { get; set; }

        public string Labels { get; set; }
    }
}
