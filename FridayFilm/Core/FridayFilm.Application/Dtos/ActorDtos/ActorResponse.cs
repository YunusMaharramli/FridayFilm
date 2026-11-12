using FridayFilm.Domain.Enums;
using System;

namespace FridayFilm.Application.DTOs.ActorsDtos;
public class ActorResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string? Nickname { get; set; }
    public string? Bio { get; set; }
    public Guid? ImageId { get; set; }
}