using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JournalMdServer.Services;
using JournalMdServer.Repositories;
using JournalMdServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.Helpers;
using JournalMdServer.DTOs;

namespace JournalMdServer.Interfaces
{
    public interface IBaseCRUDService<INP, OUTP>
    {
        public Task<ActionResult<IEnumerable<OUTP>>> GetAll(long userId);
        public Task<OUTP> GetById(long id, long userId);
        public Task<OUTP> Create(INP inputModel, long userId);
        public Task<OUTP> Update(long id, INP inputModel, long userId);
        public Task<long?> Delete(long id, long userId);
    }
}
