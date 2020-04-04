using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Goals;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class GoalsService : BaseCRUDService<Goal, GoalInput, GoalOutput>
    {
        public GoalsService(IRepository<Goal> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
