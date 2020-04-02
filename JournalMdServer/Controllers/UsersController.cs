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

        /*// PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
        }*/
    }
}
