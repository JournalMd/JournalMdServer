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
        public long NoteId { get; set; }
        public Note Note { get; set; }

        public long NoteFieldId { get; set; }
        public NoteField NoteField { get; set; }
    }
}
