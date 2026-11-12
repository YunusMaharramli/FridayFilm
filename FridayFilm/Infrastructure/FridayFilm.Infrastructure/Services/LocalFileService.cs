using FridayFilm.Application.Abstracts.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FridayFilm.Infrastructure.Services;

public sealed class LocalFileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    private static string[] allowedTypes= new[] { "image/jpeg", "image/png", "image/jpg", "image/webp" };
    public LocalFileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(string address, IFormFile file,int? size=null)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("File cannot be empty", nameof(file));

        if (!allowedTypes.Contains(file.ContentType))
            throw new ArgumentException("Invalid file type", nameof(file));

        if (size.HasValue && file.Length > size.Value)
            throw new ArgumentException($"File size cannot exceed {size.Value} bytes", nameof(file));

        string relativeDirectory = address
            .Trim()
            .TrimStart('/', '\\')
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);

        string webRootPath = _environment.WebRootPath
            ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

        string directoryPath = Path.Combine(webRootPath, relativeDirectory);
        Directory.CreateDirectory(directoryPath);

        string fileName = $"{Guid.NewGuid():N}-{file.FileName}";
        string filePath = Path.Combine(directoryPath, fileName);

        await using FileStream stream = new(
            filePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None);

        await file.CopyToAsync(stream);

        string urlPath = Path.Combine(address, fileName).Replace('\\', '/');

        return $"/{urlPath}";
    }

    public void Delete(string fileUrl)
    {
        if (string.IsNullOrWhiteSpace(fileUrl))
            return;

        string relativeDirectory = fileUrl
            .Trim()
            .TrimStart('/', '\\')
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar);
        string webRootPath = _environment.WebRootPath
            ?? Path.Combine(_environment.ContentRootPath, "wwwroot");

        string fullPath =Path.Combine(webRootPath, relativeDirectory);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
