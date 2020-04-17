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

        public string Title { get; set; }

        public string Description { get; set; }

        public string Labels { get; set; }

        // Relations
        // public ICollection<NoteValueOutput> Values { get; set; } // TODO
    }
}
