using Microsoft.AspNetCore.Http; // IFormFile üçün mütləqdir
using FridayFilm.Domain.Enums;

namespace FridayFilm.Application.DTOs.ActorsDtos;

public class CreateActorRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public Gender? Gender { get; set; }
    public string? Nickname { get; set; }
    public string? Bio { get; set; }

    // Faylı bura qəbul edəcəyik
    public IFormFile? Photo { get; set; }
}