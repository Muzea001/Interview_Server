using Interview_Server.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interview_Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Interview_Server.Repositories;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _UserRepository;
        public UserController(IRepository<User> repository )
        {
            _UserRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _UserRepository.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<User>> GetUserById(int UserId)
        {
            var user = await _UserRepository.GetByIdAsync(UserId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }




        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            await _UserRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("UserId must match");
            }
            await _UserRepository.EditAsync(user);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
             _UserRepository.deleteAsync(id);
            return NoContent();
        }


    }
}

