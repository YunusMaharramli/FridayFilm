using FluentValidation;
using FridayFilm.Application.DTOs.DirectorsDtos;

namespace FridayFilm.Application.Validators.Directors
{
    public class UpdateDirectorRequestValidator : AbstractValidator<UpdateDirectorRequest>
    {
        public UpdateDirectorRequestValidator()
        {
            // Qismən yeniləmə (partial update) olduğu üçün NotEmpty yazılmayıb.
            RuleFor(x => x.FullName)
                .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

            RuleFor(x => x.Nationality)
                .MaximumLength(100).WithMessage("Milliyət maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Bio)
                .MaximumLength(2000).WithMessage("Bioqrafiya maksimum 2000 simvol ola bilər.");

            RuleFor(x => x.Gender)
                .IsInEnum().When(x => x.Gender.HasValue).WithMessage("Düzgün cinsiyyət (Enum) seçilməyib.");
        }
    }
}