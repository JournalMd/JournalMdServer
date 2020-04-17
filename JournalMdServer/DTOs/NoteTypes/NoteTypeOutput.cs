using JournalMdServer.DTOs.NoteFields;
using JournalMdServer.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.NoteTypes
{
    public class NoteTypeOutput : IBaseOutput
    {
        public long Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // Relations
        public ICollection<NoteFieldOutput> Fields { get; set; } // TODO
    }
}
