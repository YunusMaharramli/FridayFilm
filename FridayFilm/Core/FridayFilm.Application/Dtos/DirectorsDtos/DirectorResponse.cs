using FridayFilm.Domain.Enums;
using System;

namespace FridayFilm.Application.DTOs.DirectorsDtos;

public class DirectorResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public Gender Gender { get; set; }
    public Guid? ImageId { get; set; }
}