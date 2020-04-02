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

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;

        public NotesController(NotesService service)
        {
            _notesService = service;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteOutput>>> GetNotes()
        {
            return Ok(await _notesService.GetItems(this.GetAuthenticatedId()));
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteOutput>> GetNote(long id)
        {
            var note = await _notesService.GetItem(id, this.GetAuthenticatedId());

            if (note == null)
                return NotFound();

            return note;
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<NoteOutput>> PostNote(NoteInput note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var newNote = await _notesService.Insert(note, this.GetAuthenticatedId());
            return CreatedAtAction(nameof(GetNote), new { id = newNote.Id }, newNote);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(long id, NoteInput note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                await _notesService.Update(id, note, this.GetAuthenticatedId());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteOutput>> DeleteNote(long id)
        {
            var noteId = await _notesService.Delete(id, this.GetAuthenticatedId());

            if (noteId == null)
                return NotFound();

            return Ok(new DeleteOutput() { Id = noteId.Value });
        }
    }
}
