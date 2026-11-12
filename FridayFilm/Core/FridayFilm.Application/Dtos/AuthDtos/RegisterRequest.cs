namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record RegisterRequest(
    string FullName,
    string Email,
    string Password);