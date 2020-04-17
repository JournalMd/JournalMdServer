using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Notes
{
    public class NoteInput
    {
        [Required]
        public DateTime Date { get; set; }

        // Relations
        // public long NoteTypeId { get; set; }
        // public NoteTypeOutput NoteType { get; set; }

        // public ICollection<NoteValue> NoteValues { get; set; }
        // public ICollection<NoteValueOutput> Values { get; set; } // TODO
    }
}
