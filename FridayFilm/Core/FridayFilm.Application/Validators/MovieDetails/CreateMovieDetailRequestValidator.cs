using FluentValidation;
using FridayFilm.Application.Dtos.MovieDetailDtos;

namespace FridayFilm.Application.Validators.MovieDetails
{
    public class CreateMovieDetailRequestValidator : AbstractValidator<CreateMovieDetailRequest>
    {
        public CreateMovieDetailRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıqlama boş ola bilməz.")
                .MaximumLength(2000).WithMessage("Açıqlama maksimum 2000 simvol ola bilər.");

            RuleFor(x => x.TrailerUrl)
                .NotEmpty().WithMessage("Treyler linki boş ola bilməz.")
                .MaximumLength(500).WithMessage("Treyler linki maksimum 500 simvol ola bilər.");
        }
    }
}