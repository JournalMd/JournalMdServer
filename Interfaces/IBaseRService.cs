﻿using System;
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
    public interface IBaseRService<OUTP>
    {
        public Task<ActionResult<IEnumerable<OUTP>>> GetAll();
        public Task<OUTP> GetById(long id);
    }
}
