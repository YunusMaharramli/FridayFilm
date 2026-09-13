using FluentValidation;
using FridayFilm.Application.Dtos.MovieDtos;

namespace FridayFilm.Application.Validators.Movies;

public sealed class MovieQueryRequestValidator : AbstractValidator<MovieQueryRequest>
{
    public MovieQueryRequestValidator()
    {
        Include(new FridayFilm.Application.Validators.PaginationRequestValidator());
        RuleFor(x => x.Search).MaximumLength(250);
        RuleFor(x => x.Sort).Must(x => x is "newest" or "rating" or "year" or "name");
    }
}
