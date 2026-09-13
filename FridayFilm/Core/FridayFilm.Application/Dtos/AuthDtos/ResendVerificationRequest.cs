using System.ComponentModel.DataAnnotations;

namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record ResendVerificationRequest(
    [property: Required, EmailAddress, MaxLength(256)] string Email);
