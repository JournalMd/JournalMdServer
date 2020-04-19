using JournalMdServer.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.NoteValues
{
    public class NoteValueOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Value { get; set; }

        public long NoteId { get; set; }

        public long NoteFieldId { get; set; }

        // public ICollection<NoteValueTag> NoteValueTags { get; set; }

        // public ICollection<NoteValueCategory> NoteValueCategories { get; set; }
    }
}
