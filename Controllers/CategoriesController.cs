using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Services;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Categories;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseRController<CategoriesService, CategoryOutput>
    {
        public CategoriesController(CategoriesService service) : base(service)
        {
        }

        // Intentionally left empty
    }
}
