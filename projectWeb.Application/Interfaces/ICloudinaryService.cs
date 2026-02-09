using Microsoft.AspNetCore.Http;

namespace projectWeb.Application.Interfaces;

public interface ICloudinaryService
{
    Task<string?> UploadImageAsync(IFormFile file);
    Task<string?> UploadVideoAsync(IFormFile file);
    Task DeleteFileAsync(string publicId); // Opcional, para borrar
}
