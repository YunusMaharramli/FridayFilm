using FridayFilm.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace FridayFilm.Application.DTOs.DirectorsDtos;

public class UpdateDirectorRequest 
{
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public Gender? Gender { get; set; }
    public IFormFile? Photo { get; set; }
}