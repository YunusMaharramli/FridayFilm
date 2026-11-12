using Microsoft.AspNetCore.Http;
using System;

namespace FridayFilm.Application.DTOs.BioDtos;

public class UpdateBioRequest
{
    public string? Description { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public string? LinkedInUrl { get; set; }

    // YENİ: Şəkil qəbul etmək üçün
    public IFormFile? LogoPhoto { get; set; }
}