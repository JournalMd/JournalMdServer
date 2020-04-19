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
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Mood { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Relations
        [Required]
        public long NoteTypeId { get; set; }

        public ICollection<NoteValueInput> NoteValues { get; set; }

        //TODO Tag Category
    }
}
