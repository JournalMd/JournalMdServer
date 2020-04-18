using JournalMdServer.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.DTOs.Categories
{
    public class CategoryOutput : IBaseOutput
    {
        public long Id { get; set; }

        public string Name { get; set; } // intern technical

        public string Title { get; set; }

        // Relations
        public long ParentCategoryId { get; set; }
        public CategoryOutput ParentCategory { get; set; }
    }
}
