using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Activities
{
    public class ActivityInput
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int Mood { get; set; }

        public string Labels { get; set; }
    }
}
