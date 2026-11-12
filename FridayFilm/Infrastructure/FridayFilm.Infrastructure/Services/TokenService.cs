using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.AuthDtos;
using FridayFilm.Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FridayFilm.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _options;

    public TokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public AccessTokenResult CreateAccessToken(TokenUser user)
    {
        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(
            _options.AccessTokenExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(
                JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(now).ToString(),
                ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("name", user.Name)
        };

        claims.AddRange(
            user.Roles.Select(role => new Claim("role", role)));
        claims.AddRange(
            user.Permissions.Select(permission =>
                new Claim(
                    CustomClaimTypes.Permission,
                    permission)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: now,
            expires: expiresAt,
            signingCredentials: credentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new AccessTokenResult(tokenValue, expiresAt);
    }
    public RefreshTokenResult CreateRefreshToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var token = Base64UrlEncoder.Encode(tokenBytes);

        var tokenHash = ComputeRefreshTokenHash(token);

        var expiresAtUtc = DateTime.UtcNow.AddDays(
            _options.RefreshTokenExpirationDays);

        return new RefreshTokenResult(
            token,
            tokenHash,
            expiresAtUtc);
    }

    public string ComputeRefreshTokenHash(string refreshToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

        var tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
        var hashBytes = SHA256.HashData(tokenBytes);

        return Convert.ToHexString(hashBytes);
    }
}
