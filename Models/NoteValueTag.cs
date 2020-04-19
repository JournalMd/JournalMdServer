using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    /// <summary>
    /// Solve Many-to-many relationships NoteValue - Tag
    /// </summary>
    public class NoteValueTag
    {
        public long NoteValueId { get; set; }
        public NoteValue NoteValue { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
