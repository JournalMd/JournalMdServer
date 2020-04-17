using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class NoteField : BaseEntity, IBaseModel
    {
        [Required]
        public int Order { get; set; }

        [Required]
        public string Name { get; set; } // intern technical

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        public bool Required { get; set; }

        public string Rules { get; set; }

        // Relations
        public long NoteTypeId { get; set; }
        public NoteType NoteType { get; set; }

        // owner - might be possible in the future
    }
}
