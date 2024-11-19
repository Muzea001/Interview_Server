using Microsoft.AspNetCore.Mvc;

namespace Interview_Server.Models
{
    public class FileUploadRequest
    {
        [FromForm]
        public IFormFile ProfileImage { get; set; }
    }
}
