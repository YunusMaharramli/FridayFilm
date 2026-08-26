using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Movie> Movies { get; set; }

    public Category()
    {
        Movies = new HashSet<Movie>();
    }
}