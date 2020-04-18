using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Categories;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class CategoriesService : BaseRService<Category, CategoryOutput>
    {
        public CategoriesService(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
