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
        public long NoteTypeId { get; set; }
        
        // public ICollection<NoteValueOutput> NoteValues { get; set; } // TODO
    }
}
