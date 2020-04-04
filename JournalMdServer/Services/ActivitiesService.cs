using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Activities;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class ActivitiesService : BaseCRUDService<Activity, ActivityInput, ActivityOutput>
    {
        public ActivitiesService(IRepository<Activity> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
