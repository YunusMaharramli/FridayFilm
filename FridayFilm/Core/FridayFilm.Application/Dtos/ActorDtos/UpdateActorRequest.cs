using FridayFilm.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace FridayFilm.Application.DTOs.ActorsDtos;
public class UpdateActorRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string? Nickname { get; set; }
    public string? Bio { get; set; }
    public IFormFile? Photo { get; set; } 
}