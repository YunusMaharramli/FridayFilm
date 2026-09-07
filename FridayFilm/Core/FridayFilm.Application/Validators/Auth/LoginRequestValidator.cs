using FluentValidation;
using FridayFilm.Application.Dtos.AuthDtos;

namespace FridayFilm.Application.Validators.Auth;

public sealed class LoginRequestValidator
    : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(100);
    }
}