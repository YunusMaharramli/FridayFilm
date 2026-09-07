using FridayFilm.Application.Dtos.AuthDtos;

namespace FridayFilm.Application.Abstracts.Services;

public interface ITokenService
{
    AccessTokenResult CreateAccessToken(TokenUser user);

    RefreshTokenResult CreateRefreshToken();

    string ComputeRefreshTokenHash(string refreshToken);
}