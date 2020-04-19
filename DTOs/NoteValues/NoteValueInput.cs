using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.NoteValues
{
    public class NoteValueInput
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public long NoteId { get; set; }

        [Required]
        public long NoteFieldId { get; set; }
    }
}
