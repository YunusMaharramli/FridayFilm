using FluentValidation;
using FridayFilm.Application.DTOs.BioDtos;

namespace FridayFilm.Application.Validators.Bios
{
    public class UpdateBioRequestValidator : AbstractValidator<UpdateBioRequest>
    {
        public UpdateBioRequestValidator()
        {
            // Update zamanı da eyni qaydalar keçərlidir, çünki DTO-dakı xanalar eynidir.
            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Açıqlama maksimum 2000 simvol ola bilər.");

            RuleFor(x => x.ContactPhone)
                .MaximumLength(50).WithMessage("Əlaqə nömrəsi maksimum 50 simvol ola bilər.");

            RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Düzgün e-poçt ünvanı daxil edin.")
                .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));

            RuleFor(x => x.InstagramUrl)
                .MaximumLength(250).WithMessage("Link maksimum 250 simvol ola bilər.");

            RuleFor(x => x.FacebookUrl)
                .MaximumLength(250).WithMessage("Link maksimum 250 simvol ola bilər.");

            RuleFor(x => x.TwitterUrl)
                .MaximumLength(250).WithMessage("Link maksimum 250 simvol ola bilər.");

            RuleFor(x => x.LinkedInUrl)
                .MaximumLength(250).WithMessage("Link maksimum 250 simvol ola bilər.");
        }
    }
}