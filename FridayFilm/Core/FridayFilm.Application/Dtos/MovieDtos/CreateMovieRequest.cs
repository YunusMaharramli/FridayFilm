using FridayFilm.Application.Dtos.MovieDetailDtos;

namespace FridayFilm.Application.Dtos.MovieDtos;

public class CreateMovieRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal IMDB { get; set; }
    public int Year { get; set; }
    public string CoverImg { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public Guid LanguageId { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> GenreIds { get; set; } = [];
    public List<Guid> DirectorIds { get; set; } = [];
    public List<Guid> ActorIds { get; set; } = [];
    public CreateMovieDetailRequest MovieDetail { get; set; } = new();
}
