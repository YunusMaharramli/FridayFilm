namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record AccessTokenResult(
    string Token,
    DateTime ExpiresAt);
