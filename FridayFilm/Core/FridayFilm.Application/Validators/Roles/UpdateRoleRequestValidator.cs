using FluentValidation;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.RoleDtos;

namespace FridayFilm.Application.Validators.Roles;

public sealed class UpdateRoleRequestValidator
    : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Permissions)
            .NotNull();

        RuleForEach(x => x.Permissions)
            .Must(Permissions.IsValid)
            .WithMessage("'{PropertyValue}' permission mövcud deyil.");
    }
}