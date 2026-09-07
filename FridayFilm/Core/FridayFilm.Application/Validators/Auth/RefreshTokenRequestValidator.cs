using FluentValidation;
using FridayFilm.Application.Dtos.AuthDtos;

namespace FridayFilm.Application.Validators.Auth;

public sealed class RefreshTokenRequestValidator
    : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .MaximumLength(512);
    }
}