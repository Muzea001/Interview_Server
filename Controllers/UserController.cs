﻿using Interview_Server.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Interview_Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Interview_Server.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Interview_Server.Authentication;
using Microsoft.AspNetCore.Identity;
using Interview_Server.Services;

namespace Interview_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IImageService _imageService;
        private readonly IRepository<User> _UserRepository;
        private readonly DatabaseContext _context;
        public UserController(IRepository<User> repository, DatabaseContext context, AuthService service, IImageService service1)
        {
            _UserRepository = repository;
            _context = context;
            _authService = service;
            _imageService = service1;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO dto, [FromForm] IFormFile profileImage)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email))
            {
                return BadRequest("User with these credentials already exists");
            }

            var passwordHasher = new PasswordHasher<User>();
            byte[] imageBytes = null;
            if(profileImage!= null)
            {
                try
                {
                    imageBytes = await _imageService.ProcessImageAsync(profileImage);
                }
                catch(ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
            }
            var user = new User
            {   
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHasher.HashPassword(null, dto.Password),
                Mobile = dto.Mobile,
                ProfileImage = imageBytes
                
            };

            await _UserRepository.AddAsync(user);
            return Ok("User Registered Successfully");
        }

    

        
        [HttpPost("resetPassword")]
       public async Task<ActionResult> ForgotPassword(int id,[FromBody] ResetPasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound("User with this id not found");
            }
            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null, dto.OldPassword, user.PasswordHash);
            if(verificationResult== PasswordVerificationResult.Success)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, dto.NewPassword);
                await _context.SaveChangesAsync();
                return Ok("Password has been successfully changed.");
            }
            else
            {
                return BadRequest("Invalid password");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null, user.PasswordHash, dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid Password");
            }

            var token = _authService.GenerateToken(user);
           
            return Ok(new { Token = token });

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _UserRepository.GetAllAsync(u => u.UserInterviews);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _UserRepository.GetByIdAsync(id, u => u.UserInterviews);
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
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser(int id, User user)
        {
            if (id != user.Id)
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
             await _UserRepository.deleteAsync(id);
            return NoContent();
        }


    }
}

