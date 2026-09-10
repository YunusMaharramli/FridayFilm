using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.AuthDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Persistence.Contexts;
using FridayFilm.Persistence.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FridayFilm.Persistence.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly FridayFilmDbContext _dbContext;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenticationService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager,
    ITokenService tokenService,
    FridayFilmDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
    }

    public async Task<AuthResponse> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var email = request.Email.Trim();

        var existingUser =
            await _userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            throw new ConflictException(
                "This Email is already in use.");
        }

        var user = new ApplicationUser
        {
            Fullname = request.FullName.Trim(),
            Email = email,
            UserName = email
        };

        var createResult = await _userManager.CreateAsync(
            user,
            request.Password);


        if (!createResult.Succeeded)
        {
            var errors = string.Join(
                " ",
                createResult.Errors.Select(x => x.Description));

            throw new ValidationException(errors);
        }
        await _userManager.AddToRoleAsync(user, "Admin");

        return await CreateAuthResponseAsync(
            user,
            cancellationToken);
    }

    public async Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var email = request.Email.Trim();

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new UnauthorizedException(
                "Email or Password is incorrect.");
        }

        var signInResult =
            await _signInManager.CheckPasswordSignInAsync(
                user,
                request.Password,
                lockoutOnFailure: true);

        if (signInResult.IsLockedOut)
        {
            throw new UnauthorizedException(
                "The account is temporarily locked.");
        }

        if (!signInResult.Succeeded)
        {
            throw new UnauthorizedException(
                "Email or Password is incorrect.");
        }

        return await CreateAuthResponseAsync(
            user,
            cancellationToken);
    }

    private async Task<AuthResponse> CreateAuthResponseAsync(
    ApplicationUser user,
    CancellationToken cancellationToken)
    {
        var roles =
        await _userManager.GetRolesAsync(user);

        var permissions = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var roleName in roles)
        {
            var role =
                await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                continue;
            }

            var claims =
                await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims.Where(x =>
                         x.Type ==
                         CustomClaimTypes.Permission))
            {
                permissions.Add(claim.Value);
            }
        }

        var accessToken =
            _tokenService.CreateAccessToken(
                new TokenUser(
                    user.Id,
                    user.Email!,
                    user.Fullname,
                    roles.ToArray(),
                    permissions.ToArray()));

        var refreshToken = _tokenService.CreateRefreshToken();

        _dbContext.RefreshTokens.Add(new RefreshToken
        {
            TokenHash = refreshToken.TokenHash,
            ExpiresAtUtc = refreshToken.ExpiresAtUtc,
            UserId = user.Id
        });

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            user.Id,
            user.Email!,
            accessToken.Token,
            accessToken.ExpiresAt,
            refreshToken.Token,
            refreshToken.ExpiresAtUtc);
    }
    public async Task<AuthResponse> RefreshAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = _tokenService.ComputeRefreshTokenHash(
            request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken);

        var now = DateTime.UtcNow;

        if (storedToken is null ||
            storedToken.RevokedAtUtc is not null ||
            storedToken.ExpiresAtUtc <= now)
        {
            throw new UnauthorizedException(
                "Refresh token is invalid or expired.");
        }

        storedToken.RevokedAtUtc = now;

        return await CreateAuthResponseAsync(
            storedToken.User,
            cancellationToken);
    }
    public async Task LogoutAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = _tokenService.ComputeRefreshTokenHash(
            request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(
                x => x.TokenHash == tokenHash,
                cancellationToken);

        if (storedToken is null ||
            storedToken.RevokedAtUtc is not null)
        {
            return;
        }

        storedToken.RevokedAtUtc = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}
