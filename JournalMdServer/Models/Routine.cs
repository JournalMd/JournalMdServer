using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Routine : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Labels { get; set; }
    }
}
