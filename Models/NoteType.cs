using JournalMdServer.Interfaces.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class NoteType : BaseEntity, IBaseModel
    {
        [Required]
        public int Order { get; set; }

        [Required]
        public string Name { get; set; } // intern technical

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        // Relations
        public ICollection<NoteField> Fields { get; set; }

        // owner - might be possible in the future
    }
}
