namespace FridayFilm.Application.Dtos.AuthDtos;


public sealed record AuthResponse(
    string UserId,
    string Email,
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);