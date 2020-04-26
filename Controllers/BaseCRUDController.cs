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
    public class BaseCRUDController<S, INP, OUTP> : ControllerBase where S : IBaseCRUDService<INP, OUTP> where OUTP : IBaseOutput
    {
        private readonly S _service;

        public BaseCRUDController(S service)
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

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<OUTP>> Create(INP model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                var newEntitie = await _service.Create(model, this.GetAuthenticatedId());
                return CreatedAtAction(nameof(Create), new { id = newEntitie.Id }, newEntitie);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, INP model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                await _service.Update(id, model, this.GetAuthenticatedId());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

            return NoContent();
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteOutput>> Delete(long id)
        {
            var entitieId = await _service.Delete(id, this.GetAuthenticatedId());

            if (entitieId == null)
                return NotFound();

            return Ok(new DeleteOutput() { Id = entitieId.Value });
        }

    }
}
