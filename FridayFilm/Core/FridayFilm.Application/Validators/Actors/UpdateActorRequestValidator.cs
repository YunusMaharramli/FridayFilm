using FluentValidation;
using FridayFilm.Application.DTOs.ActorsDtos;

namespace FridayFilm.Application.Validators.Actors
{
    public class UpdateActorRequestValidator : AbstractValidator<UpdateActorRequest>
    {
        public UpdateActorRequestValidator()
        {
            // QEYD: UpdateService-də "qismən yeniləmə" (partial update) yazdığımız üçün
            // burada NotEmpty() istifadə etmirik. Front-end məlumatı boş göndərə bilər.
            // Yalnız məlumat göndərilibsə, onun uzunluğunu və formatını yoxlayırıq.

            RuleFor(x => x.FullName)
                .MaximumLength(150).WithMessage("Ad maksimum 150 simvol ola bilər.");

            RuleFor(x => x.Nationality)
                .MaximumLength(100).WithMessage("Milliyət maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Nickname)
                .MaximumLength(100).WithMessage("Ləqəb maksimum 100 simvol ola bilər.");

            RuleFor(x => x.Bio)
                .MaximumLength(2000).WithMessage("Bioqrafiya maksimum 2000 simvol ola bilər.");

            RuleFor(x => x.Gender)
                .IsInEnum().When(x => x.Gender.HasValue).WithMessage("Düzgün cinsiyyət (Enum) seçilməyib.");
        }
    }
}