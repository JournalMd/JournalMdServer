using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Habits
{
    public class HabitInput
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Labels { get; set; }
    }
}
