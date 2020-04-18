using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JournalMdServer.Models;
using JournalMdServer.Services;
using JournalMdServer.Helpers;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Users;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService service)
        {
            _usersService = service;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginOutput>> Authenticate([FromBody]LoginInput model)
        {
            var user = await _usersService.Authenticate(model);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterOutput>> Register([FromBody]RegisterInput model)
        {
            try
            {
                var user = await _usersService.Create(model);
                return CreatedAtAction(nameof(GetUsers), new { }, user);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<UserOutput>> GetUsers()
        {
            var user = await _usersService.GetById(this.GetAuthenticatedId());

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users
        [HttpPut]
        public async Task<IActionResult> Update(UserInput model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                await _usersService.Update(model, this.GetAuthenticatedId());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

            return NoContent();
        }

        // PUT: api/Users
        [HttpPut("password")]
        public async Task<IActionResult> Update(UpdatePasswordInput model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            try
            {
                await _usersService.UpdatePassword(model, this.GetAuthenticatedId());
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

            return NoContent();
        }
    }
}
