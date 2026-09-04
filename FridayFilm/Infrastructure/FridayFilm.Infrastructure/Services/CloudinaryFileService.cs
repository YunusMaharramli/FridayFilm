using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FridayFilm.Infrastructure.Services;

public sealed class CloudinaryFileService : IFileService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryFileService(
        IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(
            settings.CloudName,
            settings.ApiKey,
            settings.ApiSecret);

        _cloudinary = new Cloudinary(account)
        {
            Api =
            {
                Secure = true
            }
        };
    }

    public async Task<string> UploadAsync(
        string address,
        IFormFile file,
        int? size = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(address);
        ArgumentNullException.ThrowIfNull(file);

        if (file.Length == 0)
            throw new ArgumentException("Fayl boş ola bilməz.", nameof(file));

        if (size is <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(size),
                "Ölçü sıfırdan böyük olmalıdır.");

        await using var stream = file.OpenReadStream();

        var publicId = $"{Guid.NewGuid():N}";

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = NormalizeFolder(address),
            PublicId = publicId,
            UniqueFilename = false,
            Overwrite = false,
            UseFilename = false
        };

        if (size.HasValue)
        {
            uploadParams.Transformation = new Transformation()
                .Width(size.Value)
                .Height(size.Value)
                .Crop("limit")
                .Quality("auto")
                .FetchFormat("auto");
        }

        var result = await _cloudinary.UploadAsync(uploadParams);

        if (result.Error is not null)
        {
            throw new InvalidOperationException(
                $"Cloudinary upload xətası: {result.Error.Message}");
        }

        if (result.SecureUrl is null)
            throw new InvalidOperationException(
                "Cloudinary fayl URL-ni qaytarmadı.");

        return result.SecureUrl.AbsoluteUri;
    }

    public void Delete(string fileUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileUrl);

        var publicId = GetPublicIdFromUrl(fileUrl);

        var deleteParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Image,
            Type = "upload",
            Invalidate = true
        };

        var result = _cloudinary.Destroy(deleteParams);

        if (result.Error is not null)
        {
            throw new InvalidOperationException(
                $"Cloudinary delete xətası: {result.Error.Message}");
        }

        if (result.Result is not ("ok" or "not found"))
        {
            throw new InvalidOperationException(
                $"Fayl silinmədi. Cloudinary nəticəsi: {result.Result}");
        }
    }

    private static string NormalizeFolder(string address)
    {
        var folder = address
            .Trim()
            .Replace('\\', '/')
            .Trim('/');

        if (string.IsNullOrWhiteSpace(folder))
            throw new ArgumentException(
                "Cloudinary folder adı boş ola bilməz.",
                nameof(address));

        return folder;
    }

    private static string GetPublicIdFromUrl(string fileUrl)
    {
        if (!Uri.TryCreate(fileUrl, UriKind.Absolute, out var uri))
            throw new ArgumentException(
                "Düzgün fayl URL-i daxil edilməyib.",
                nameof(fileUrl));

        var path = Uri.UnescapeDataString(uri.AbsolutePath);
        const string uploadMarker = "/upload/";

        var uploadIndex = path.IndexOf(
            uploadMarker,
            StringComparison.OrdinalIgnoreCase);

        if (uploadIndex < 0)
            throw new ArgumentException(
                "URL Cloudinary upload URL-i deyil.",
                nameof(fileUrl));

        var publicId = path[(uploadIndex + uploadMarker.Length)..];

        var segments = publicId.Split(
            '/',
            StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length > 0 &&
            segments[0].Length > 1 &&
            segments[0][0] == 'v' &&
            segments[0][1..].All(char.IsDigit))
        {
            publicId = string.Join('/', segments.Skip(1));
        }

        var extensionIndex = publicId.LastIndexOf('.');

        if (extensionIndex > publicId.LastIndexOf('/'))
            publicId = publicId[..extensionIndex];

        if (string.IsNullOrWhiteSpace(publicId))
            throw new ArgumentException(
                "Cloudinary public ID URL-dən çıxarıla bilmədi.",
                nameof(fileUrl));

        return publicId;
    }
}
