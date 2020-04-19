using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    /// <summary>
    /// Solve Many-to-many relationships Note - Category
    /// </summary>
    public class NoteCategory
    {
        public long NoteId { get; set; }
        public Note Note { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
