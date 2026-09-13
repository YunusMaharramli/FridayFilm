using FluentValidation;
using FridayFilm.Application.Pagination;

namespace FridayFilm.Application.Validators;

public sealed class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Size).InclusiveBetween(1, 100);
        RuleFor(x => x).Must(x => ((long)x.Page - 1) * x.Size <= int.MaxValue)
            .WithMessage("Səhifə nömrəsi çox böyükdür.");
    }
}
