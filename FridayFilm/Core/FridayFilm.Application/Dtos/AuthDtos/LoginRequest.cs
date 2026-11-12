namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record LoginRequest(
    string Email,
    string Password);