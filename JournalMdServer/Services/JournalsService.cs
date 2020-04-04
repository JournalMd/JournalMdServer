using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.Journals;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class JournalsService : BaseCRUDService<Journal, JournalInput, JournalOutput>
    {
        public JournalsService(IRepository<Journal> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        // Intentionally left empty
    }
}
