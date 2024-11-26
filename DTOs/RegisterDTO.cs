using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

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

            [Required]
            public string ProfileImage { get; set; }
        
    }
}
