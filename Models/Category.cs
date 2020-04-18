using JournalMdServer.Interfaces.Models;
using System.ComponentModel.DataAnnotations;

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

        // owner - might be possible in the future
    }
}
