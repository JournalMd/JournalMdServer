using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Models
{
    /// <summary>
    /// Solve Many-to-many relationships NoteValue - Category
    /// </summary>
    public class NoteValueCategory
    {
        public long NoteValueId { get; set; }
        public NoteValue NoteValue { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
