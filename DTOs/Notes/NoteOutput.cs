using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.DTOs.Notes
{
    public class NoteOutput : IBaseOutput
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        // Relations
        // public long NoteTypeId { get; set; }
        // public NoteTypeOutput NoteType { get; set; }

        // public ICollection<NoteValue> NoteValues { get; set; }
        // public ICollection<NoteValueOutput> Values { get; set; } // TODO
    }
}
