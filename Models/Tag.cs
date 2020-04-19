using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JournalMdServer.Models
{
    public class Tag : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Name { get; set; } // intern technical

        [Required]
        public string Title { get; set; }

        // Relations
        public ICollection<NoteTag> NoteTags { get; set; }
    }
}
