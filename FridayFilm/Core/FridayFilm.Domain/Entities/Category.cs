using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public List<Movie> Movies { get; set; } = new();

  
}