using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Repositories;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using JournalMdServer.DTOs.Notes;
using AutoMapper;
using JournalMdServer.Helpers;
using System.Web.Http.ModelBinding;
using Task = System.Threading.Tasks.Task;

namespace JournalMdServer.Services
{
    public class BaseRService<TEntity, OUTP> : IBaseRService<OUTP> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseRService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<OUTP>>> GetAll(long userId)
        {
            var entities = await _repository.Query.ToListAsync();
            return _mapper.Map<List<OUTP>>(entities);
            //var notes = _repository.Query.GetPaged(1, 3);
            //return _mapper.Map<PagedResult<NoteOutput>>(notes);
        }

        public async Task<OUTP> GetById(long id, long userId)
        {
            var entitie = await _repository.Query.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<OUTP>(entitie);
        }
    }
}
