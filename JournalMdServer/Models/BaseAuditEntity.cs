using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    public class BaseAuditEntity : BaseEntity
    {
        public long CreatedById { get; set; }

        public long UpdatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // FK
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
