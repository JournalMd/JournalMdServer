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
using JournalMdServer.DTOs.WeightMeasurements;
using JournalMdServer.Helpers;
using JournalMdServer.DTOs;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeightMeasurementsController : ControllerBase
    {
        private readonly WeightMeasurementsService _weightMeasurementsService;

        public WeightMeasurementsController(WeightMeasurementsService service)
        {
            _weightMeasurementsService = service;
        }

        // GET: api/WeightMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeightMeasurementOutput>>> GetWeightMeasurements()
        {
            return Ok(await _weightMeasurementsService.GetAll(this.GetAuthenticatedId()));
        }

        // GET: api/WeightMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeightMeasurementOutput>> GetWeightMeasurement(long id)
        {
            var weightMeasurement = await _weightMeasurementsService.GetById(id, this.GetAuthenticatedId());

            if (weightMeasurement == null)
                return NotFound();

            return weightMeasurement;
        }

        // POST: api/WeightMeasurements
        [HttpPost]
        public async Task<ActionResult<WeightMeasurementOutput>> PostWeightMeasurement(WeightMeasurementInput weightMeasurement)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var newWeightMeasurement = await _weightMeasurementsService.Create(weightMeasurement, this.GetAuthenticatedId());
            return CreatedAtAction(nameof(GetWeightMeasurement), new { id = newWeightMeasurement.Id }, newWeightMeasurement);
        }

        // PUT: api/WeightMeasurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeightMeasurement(long id, WeightMeasurementInput weightMeasurement)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                await _weightMeasurementsService.Update(id, weightMeasurement, this.GetAuthenticatedId());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

            return NoContent();
        }

        // DELETE: api/WeightMeasurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteOutput>> DeleteWeightMeasurement(long id)
        {
            var weightMeasurementId = await _weightMeasurementsService.Delete(id, this.GetAuthenticatedId());

            if (weightMeasurementId == null)
                return NotFound();

            return Ok(new DeleteOutput() { Id = weightMeasurementId.Value });
        }
    }
}
