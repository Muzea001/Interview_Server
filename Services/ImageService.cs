using Interview_Server.Interfaces;

namespace Interview_Server.Services
{
    public class ImageService : IImageService
    {
        public async Task<byte[]> ProcessImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("Invalid image file");
            }
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
