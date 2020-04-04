using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Note : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
