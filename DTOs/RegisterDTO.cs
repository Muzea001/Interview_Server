using System.ComponentModel.DataAnnotations;

namespace Interview_Server.DTOs
{
    public class RegisterDTO
    {

            [Required]
            public string Username { get; set; }

           [Required]
            public string Password { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Mobile { get; set; }
        
    }
}
