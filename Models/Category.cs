using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JournalMdServer.Models
{
    public class Category : BaseEntity, IBaseModel
    {
        [Required]
        public string Name { get; set; } // intern technical

        [Required]
        public string Title { get; set; }

        // Relations
        public long? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<NoteValueCategory> NoteValueCategory { get; set; }

        // owner - might be possible in the future
    }
}
