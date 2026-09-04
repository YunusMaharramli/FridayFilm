using FluentValidation;
using FridayFilm.Application.Dtos.GenreDtos;

namespace FridayFilm.Application.Validators.Genres
{
    public class CreateGenreRequestValidator : AbstractValidator<CreateGenreRequest> // Create DTO-n necə adlanırsa onu yaz
    {
        public CreateGenreRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Janr adı boş ola bilməz.")
                .MaximumLength(100).WithMessage("Janr adı maksimum 100 simvol ola bilər.");
        }
    }
}