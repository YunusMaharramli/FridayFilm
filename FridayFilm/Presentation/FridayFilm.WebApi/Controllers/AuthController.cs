using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.AuthDtos;
using FridayFilm.Persistence.Users; // ApplicationUser üçün əlavə edildi
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; // UserManager üçün əlavə edildi
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FridayFilm.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("auth")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService.RegisterAsync(
                request,
                cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService.LoginAsync(
                request,
                cancellationToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    [FromBody] RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        await _authenticationService.LogoutAsync(
            request,
            cancellationToken);

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
    [FromBody] RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService.RefreshAsync(
                request,
                cancellationToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("resend-verification")]
    [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("auth")]
    public async Task<IActionResult> Resend(ResendVerificationRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.ResendVerificationAsync(request.Email, cancellationToken);
        return Ok(new { Message = "Təsdiqlənməmiş hesab varsa, məktub göndərildi." });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId =
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        var email =
            User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

        var name =
            User.FindFirst("name")?.Value;

        return Ok(new
        {
            UserId = userId,
            Email = email,
            Name = name
        });
    }

    // ==========================================
    // YENİ ƏLAVƏ EDİLƏN E-POÇT TƏSDİQLƏMƏ QAPISI
    // ==========================================
    [AllowAnonymous]
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail(
        [FromQuery] string userId,
        [FromQuery] string token,
        CancellationToken cancellationToken)
    {
        // Bütün yoxlama işini və xətaları Servis həll edir!
        await _authenticationService.VerifyEmailAsync(userId, token, cancellationToken);

        // Əgər bura gəlib çatdısa, deməli Servis xəta fırlatmayıb və hər şey uğurludur
        return Ok("Təbriklər! E-poçtunuz təsdiqləndi. Artıq hesabınıza daxil (Login) ola bilərsiniz.");
    }
}
