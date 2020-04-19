using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JournalMdServer.Models
{
    public class NoteValue : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Value { get; set; }

        // Relations
        // public long NoteId { get; set; } // Navigate by NoteField
        // public Note Note { get; set; } // Navigate by NoteField

        public long NoteFieldId { get; set; }
        public NoteField NoteField { get; set; }

        public ICollection<NoteValueTag> NoteValueTags { get; set; }

        public ICollection<NoteValueCategory> NoteValueCategories { get; set; }
    }
}
