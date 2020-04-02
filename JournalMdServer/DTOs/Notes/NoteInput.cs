using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Notes
{
    public class NoteInput
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
