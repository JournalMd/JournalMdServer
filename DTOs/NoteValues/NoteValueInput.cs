using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.NoteValues
{
    public class NoteValueInput
    {
        // [Required]
        // public long Id { get; set; } // must be set to 0 in create to prevent forcing ids by user!!!
        // Don't use at all. There can only be one NoteFieldId for every NoteType - get the fields by that!

        public string Value { get; set; }

        [Required]
        public long NoteFieldId { get; set; }
    }
}
