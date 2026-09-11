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
    private readonly UserManager<ApplicationUser> _userManager; // Yeni

    public AuthController(
        IAuthenticationService authenticationService,
        UserManager<ApplicationUser> userManager) // Yeni
    {
        _authenticationService = authenticationService;
        _userManager = userManager;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var response =
            await _authenticationService.RegisterAsync(
                request,
                cancellationToken);

        // Qeyd: Əgər sən AuthService-də "return null" etmisənsə, 
        // bura gələn response null olacaq. Bu normaldır.
        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
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