using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Authorization;
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
}