using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Goals
{
    public class GoalInput
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public DateTime Due { get; set; }

        public string Labels { get; set; }
    }
}
