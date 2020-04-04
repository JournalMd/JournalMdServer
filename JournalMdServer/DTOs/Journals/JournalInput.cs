using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Journals
{
    public class JournalInput
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int Mood { get; set; }

        public string Labels { get; set; }
    }
}
