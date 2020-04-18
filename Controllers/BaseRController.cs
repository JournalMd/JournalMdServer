using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JournalMdServer.Models;
using JournalMdServer.Services;
using JournalMdServer.Repositories;
using JournalMdServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.Helpers;
using JournalMdServer.DTOs;
using JournalMdServer.Interfaces.DTOs;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseRController<S, OUTP> : ControllerBase where S : IBaseRService<OUTP> where OUTP : IBaseOutput
    {
        private readonly S _service;

        public BaseRController(S service)
        {
            _service = service;
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<OUTP>>> GetAll()
        {
            return Ok(await _service.GetAll(this.GetAuthenticatedId()));
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OUTP>> GetById(long id)
        {
            var note = await _service.GetById(id, this.GetAuthenticatedId());

            if (note == null)
                return NotFound();

            return note;
        }
    }
}
