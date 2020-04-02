using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Note : BaseEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        // FK
        public long UserId { get; set; }
        public User User{ get; set; }
    }
}
