namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record RegisterResponse(
    string Email,
    string Message);