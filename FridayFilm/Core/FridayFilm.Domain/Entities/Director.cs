using FridayFilm.Domain.Common;
using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;

public class Director : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public Gender Gender { get; set; }
    public List<Movie> Movies { get; set; } = new();
    public FilmImage? Image { get; set; }
    public Guid? ImageId { get; set; }
}