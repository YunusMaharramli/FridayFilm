namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record RefreshTokenResult(
    string Token,
    string TokenHash,
    DateTime ExpiresAtUtc);