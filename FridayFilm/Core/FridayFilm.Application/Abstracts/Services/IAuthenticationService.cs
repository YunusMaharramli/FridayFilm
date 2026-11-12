using FridayFilm.Application.Dtos.AuthDtos;

namespace FridayFilm.Application.Abstracts.Services;

public interface IAuthenticationService
{
    Task<RegisterResponse> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default);

    Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
    Task LogoutAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken = default);
    Task<AuthResponse> RefreshAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken = default);
    Task VerifyEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
    Task ResendVerificationAsync(string email, CancellationToken cancellationToken = default);
}
