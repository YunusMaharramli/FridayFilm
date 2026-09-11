using FluentValidation;
using FridayFilm.Application.Dtos.MovieDtos;
using FridayFilm.Application.Validators.MovieDetails;

namespace FridayFilm.Application.Validators.Movies;

public sealed class CreateMovieRequestValidator : AbstractValidator<CreateMovieRequest>
{
    public CreateMovieRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.IMDB).InclusiveBetween(0m, 10m)
            .Must(value => decimal.Round(value, 1) == value)
            .WithMessage("IMDB maksimum bir onluq rəqəm ola bilər.");
        RuleFor(x => x.Year).InclusiveBetween(1, 9999);
        RuleFor(x => x.CoverImg).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Duration).GreaterThan(TimeSpan.Zero);
        RuleFor(x => x.LanguageId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.GenreIds).NotNull();
        RuleFor(x => x.DirectorIds).NotNull();
        RuleFor(x => x.ActorIds).NotNull();
        RuleForEach(x => x.GenreIds).NotEmpty();
        RuleForEach(x => x.DirectorIds).NotEmpty();
        RuleForEach(x => x.ActorIds).NotEmpty();
        RuleFor(x => x.MovieDetail).NotNull()
            .SetValidator(new CreateMovieDetailRequestValidator());
    }
}
