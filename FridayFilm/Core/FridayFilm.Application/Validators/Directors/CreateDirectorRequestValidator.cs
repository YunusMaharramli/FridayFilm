using FluentValidation;
using FridayFilm.Application.DTOs.DirectorsDtos;

namespace FridayFilm.Application.Validators.Directors
{
    public class CreateDirectorRequestValidator : AbstractValidator<CreateDirectorRequest>
    {
        public CreateDirectorRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Rejissorun adı boş ola bilməz.")
                .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Milliyət boş ola bilməz.")
                .MaximumLength(100).WithMessage("Milliyət maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Bio)
                .MaximumLength(2000).WithMessage("Bioqrafiya maksimum 2000 simvol ola bilər.");

            RuleFor(x => x.Gender)
                .IsInEnum().When(x => x.Gender.HasValue).WithMessage("Düzgün cinsiyyət (Enum) seçilməyib.");
        }
    }
}