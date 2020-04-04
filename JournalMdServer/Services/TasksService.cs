using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Tasks;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class TasksService : BaseCRUDService<Task, TaskInput, TaskOutput>
    {
        public TasksService(IRepository<Task> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
