using JournalMdServer.DTOs.NoteValues;
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
        [MinLength(3)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, 5)]
        public int Mood { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Relations
        [Required]
        public long NoteTypeId { get; set; }

        public ICollection<NoteValueInput> NoteValues { get; set; }

        // Pseudo relation
        public ICollection<long> Categories { get; set; }

        public ICollection<long> Tags { get; set; }
    }
}
