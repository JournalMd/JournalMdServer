using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using JournalMdServer.DTOs.NoteTypes;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace JournalMdServer.Services
{
    public class NoteTypesService : BaseRService<NoteType, NoteTypeOutput>
    {
        public NoteTypesService(IRepository<NoteType> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<ActionResult<IEnumerable<NoteTypeOutput>>> GetAll()
        {
            var entities = await _repository.Query.Include(inc => inc.NoteFields).ToListAsync();
            return _mapper.Map<List<NoteTypeOutput>>(entities);
        }
    }
}
