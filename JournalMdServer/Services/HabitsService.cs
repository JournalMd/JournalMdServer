using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Habits;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class HabitsService : BaseCRUDService<Habit, HabitInput, HabitOutput>
    {
        public HabitsService(IRepository<Habit> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
