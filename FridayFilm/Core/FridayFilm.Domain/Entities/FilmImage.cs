using System;
using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities;

public class FilmImage : BaseEntity
{
    public string PhotoUrl { get; set; } = string.Empty;
    public Guid MovieId { get; set; }
}