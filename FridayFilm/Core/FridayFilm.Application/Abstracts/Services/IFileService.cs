using Microsoft.AspNetCore.Http;

namespace FridayFilm.Application.Abstracts.Services;

public interface IFileService
{
    Task<string> UploadAsync(string address, IFormFile file, int? size = null);
    void Delete(string fileUrl);
}
