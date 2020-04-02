using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Notes
{
    public class NoteOutput
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
