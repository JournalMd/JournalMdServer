using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    /// <summary>
    /// Solve Many-to-many relationships Note - Tag
    /// </summary>
    public class NoteTag
    {
        public long NoteId { get; set; }
        public Note Note { get; set; }

        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
