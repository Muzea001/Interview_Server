namespace Interview_Server.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> ProcessImageAsync(IFormFile image);
    }

}
