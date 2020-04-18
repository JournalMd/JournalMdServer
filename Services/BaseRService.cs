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
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseRService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ActionResult<IEnumerable<OUTP>>> GetAll()
        {
            var entities = await _repository.Query.ToListAsync();
            return _mapper.Map<List<OUTP>>(entities);
        }

        public virtual async Task<OUTP> GetById(long id)
        {
            var entitie = await _repository.Query.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<OUTP>(entitie);
        }
    }
}
