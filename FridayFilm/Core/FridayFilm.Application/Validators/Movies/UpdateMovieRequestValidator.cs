using FluentValidation;
using FridayFilm.Application.Dtos.MovieDtos;

namespace FridayFilm.Application.Validators.Movies;

public sealed class UpdateMovieRequestValidator : AbstractValidator<UpdateMovieRequest>
{
    public UpdateMovieRequestValidator()
    {
        Include(new CreateMovieRequestValidator());
    }
}
