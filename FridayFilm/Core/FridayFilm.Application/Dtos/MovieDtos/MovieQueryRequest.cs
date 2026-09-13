using FridayFilm.Application.Pagination;
using System.ComponentModel.DataAnnotations;

namespace FridayFilm.Application.Dtos.MovieDtos;

public sealed class MovieQueryRequest : PaginationRequest
{
    [MaxLength(250)] public string? Search { get; set; }
    public Guid? CategoryId { get; set; }
    [RegularExpression("^(newest|rating|year|name)$")] public string Sort { get; set; } = "newest";
}
