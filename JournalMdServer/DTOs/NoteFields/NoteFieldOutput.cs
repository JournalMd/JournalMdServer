using JournalMdServer.DTOs.NoteTypes;
using JournalMdServer.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.NoteFields
{
    public class NoteFieldOutput : IBaseOutput
    {
        public long Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; } // intern technical

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public bool Required { get; set; }

        public string Rules { get; set; }

        // Relations
        public long NoteTypeId { get; set; }
        public NoteTypeOutput NoteType { get; set; }
    }
}
