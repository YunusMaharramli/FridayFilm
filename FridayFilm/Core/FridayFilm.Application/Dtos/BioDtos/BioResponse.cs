using System;

namespace FridayFilm.Application.DTOs.BioDtos;

public class BioResponse
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public Guid? LogoId { get; set; }
}