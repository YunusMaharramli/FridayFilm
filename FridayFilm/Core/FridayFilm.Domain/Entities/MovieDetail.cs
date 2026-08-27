using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities;

public class MovieDetail : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}