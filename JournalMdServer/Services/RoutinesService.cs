using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Routines;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class RoutinesService : BaseCRUDService<Routine, RoutineInput, RoutineOutput>
    {
        public RoutinesService(IRepository<Routine> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
