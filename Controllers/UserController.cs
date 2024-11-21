using Interview_Server.Interfaces;
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
        private readonly IEmailService _emailService;
        private readonly AuthService _authService;
        private readonly AuthValidationService _validationService;
        private readonly IImageService _imageService;
        private readonly IRepository<User> _UserRepository;
        private readonly DatabaseContext _context;
        public UserController(IRepository<User> repository, DatabaseContext context, AuthService service, AuthValidationService validationService, IImageService service1, IEmailService emailService)
        {
            _UserRepository = repository;
            _context = context;
            _authService = service;
            _validationService = validationService;
            _imageService = service1;
            _emailService = emailService;
        }

        [HttpPost("Register")]
        
        public async Task<ActionResult> Register(RegisterDTO dto)
        {
                var errors = new List<String>();

                if (!_validationService.ValidateUserName(dto.Username))
                {
                    errors.Add("Invalid Username. Must be alphanumeric and 3-15 characters");
                }

                if (!_validationService.ValidatePassword(dto.Password))
                {

                    errors.Add("Invalid Password. Must be at least 8 characters with uppercase, lowercase, digit, and special character.");

                }

                if (!_validationService.ValidateEmail(dto.Email))
                {
                    errors.Add("Invalid Email format");
                }

                if (!_validationService.ValidateMobile(dto.Mobile))
                {
                    errors.Add("Invalid Mobile number. Must be 8 digits");
                }

                if (await _context.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email))
            {
                return NotFound("User with this email not found");
            }

            if ((errors.Any()))
            {
                return BadRequest(new { Errors = errors });
            }
           
            if ((errors.Any())){
               return BadRequest(new { Errors = errors } );
            }

            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "SeedImages");
            var defaultImagePath = Path.Combine(imageDirectory, "default1.jpg");
            var passwordHasher = new PasswordHasher<User>();
            var defaultImageBytes = System.IO.File.ReadAllBytes(defaultImagePath);

            if (user == null)
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHasher.HashPassword(null, dto.Password),
                Mobile = dto.Mobile,
                ProfileImage = defaultImageBytes

            };

            await _UserRepository.AddAsync(user);
            return Ok("User Registered Successfully");
        }

        [HttpPost("{userId}/UploadProfileImage")]
        public async Task<ActionResult> UploadProfileImage(int userId, [FromForm] FileUploadRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (request.ProfileImage == null || request.ProfileImage.Length == 0)
            {
                return BadRequest("Invalid image file");
            }

            using (var memoryStream = new MemoryStream())
            {
                await request.ProfileImage.CopyToAsync(memoryStream);
                user.ProfileImage = memoryStream.ToArray(); 
            }

            await _context.SaveChangesAsync();
            return Ok("Profile image uploaded successfully");
        }


        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            try
            {
                // Find the user by email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return NotFound("User with this email not found");
                }

                var resetToken = Guid.NewGuid().ToString();
                user.ResetToken = resetToken;
                user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(15); 
                await _context.SaveChangesAsync();

                var mailData = new MailData
                {
                    MailTo = user.Email,
                    MailToId = user.Username, 
                    Subject = "Password Reset Request",
                    Body = $"Your password reset code is: {resetToken}"
                };

                var emailSent = await _emailService.SendEmailAsync(mailData);

                if (!emailSent)
                {
                    return StatusCode(500, "Error sending email. Please try again later.");
                }

                return Ok("A password reset code has been sent to your email.");
            }
            catch (Exception ex)
            {
                // Log error (optional)
                Console.WriteLine($"Error in ForgotPassword: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [Authorize]
        [HttpPost("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == dto.ResetToken);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired reset token.");
            }

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, dto.NewPassword);

            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            await _context.SaveChangesAsync();

            return Ok("Password has been successfully reset.");
        }

        [HttpPost("changePassword")]
       public async Task<ActionResult> ChangePassword(int id,[FromBody] ChangePasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound("User with this id not found");
            }
            var passwordHasher = new PasswordHasher<User>();
            var oldPassHash = passwordHasher.HashPassword(user, dto.OldPassword);
            var verificationResult = passwordHasher.VerifyHashedPassword(null,oldPassHash,user.PasswordHash);
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

