using JournalMdServer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Note : BaseAuditEntity, IBaseModel
    {
        [Required]
        public DateTime Date { get; set; }

        // Relations
        [Required]
        public long NoteTypeId { get; set; }
        public NoteType NoteType { get; set; }

        public ICollection<NoteValue> NoteValues { get; set; }
    }
}
