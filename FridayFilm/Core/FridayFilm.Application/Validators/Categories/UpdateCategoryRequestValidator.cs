using FluentValidation;
using FridayFilm.Application.Dtos.CategoryDtos;

namespace FridayFilm.Application.Validators.Categories
{
    public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
                .MaximumLength(100).WithMessage("Kateqoriya adı maksimum 100 simvol ola bilər.");
        }
    }
}