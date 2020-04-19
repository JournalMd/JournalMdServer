using JournalMdServer.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JournalMdServer.Models
{
    public class Note : BaseAuditEntity, IBaseModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Mood { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Relations
        [Required]
        public long NoteTypeId { get; set; }
        public NoteType NoteType { get; set; }

        public ICollection<NoteValue> NoteValues { get; set; }

        public ICollection<NoteTag> NoteTags { get; set; }

        public ICollection<NoteCategory> NoteCategories { get; set; }
    }
}
