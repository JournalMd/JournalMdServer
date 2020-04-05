using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Journal : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Mood { get; set; }

        public string Labels { get; set; }

        // TODO Images
    }
}
