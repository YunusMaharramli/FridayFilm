using FridayFilm.Application.Abstracts.Notifications;
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
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly FridayFilmDbContext _dbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly Microsoft.Extensions.Logging.ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService,
        IEmailService emailService,
        FridayFilmDbContext dbContext,
        Microsoft.Extensions.Logging.ILogger<AuthenticationService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<RegisterResponse> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var email = request.Email.Trim();

        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            throw new ConflictException("This email is already in use.");
        }

        var user = new ApplicationUser
        {
            Fullname = request.FullName.Trim(),
            Email = email,
            UserName = email
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        var createResult =
            await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            var errors = string.Join(
                " ",
                createResult.Errors.Select(x => x.Description));

            throw new ValidationException(errors);
        }

        var roleResult =
            await _userManager.AddToRoleAsync(user, AppRoles.User);

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(
                " ",
                roleResult.Errors.Select(x => x.Description));

            throw new ValidationException(errors);
        }

        await transaction.CommitAsync(cancellationToken);
        var token =
            await _userManager.GenerateEmailConfirmationTokenAsync(user);

        try
        {
            await _emailService.SendVerificationEmailAsync(user.Email!, user.Id, token);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            Microsoft.Extensions.Logging.LoggerExtensions.LogError(_logger, exception,
                "Verification email delivery failed after account creation.");
            return new RegisterResponse(email, "Hesab yaradıldı, amma təsdiq məktubu göndərilmədi. Bir az sonra 'Emaili təsdiqlə' bölməsindən yenidən məktub istəyin.");
        }

        return new RegisterResponse(
            user.Email!,
            "Registration was successful. Please verify your email.");
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
            throw new UnauthorizedException("Email or Password is incorrect.");
        }

        // ==========================================
        // YENİ: Əgər e-poçt təsdiqlənməyibsə, sistemə buraxmırıq!
        // ==========================================
        if (!user.EmailConfirmed)
        {
            throw new UnauthorizedException("Hesabınıza daxil olmaq üçün zəhmət olmasa e-poçtunuzu təsdiqləyin.");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        if (signInResult.IsLockedOut)
        {
            throw new UnauthorizedException("The account is temporarily locked.");
        }

        if (!signInResult.Succeeded)
        {
            throw new UnauthorizedException("Email or Password is incorrect.");
        }

        return await CreateAuthResponseAsync(user, cancellationToken);
    }

    private async Task<AuthResponse> CreateAuthResponseAsync(
        ApplicationUser user,
        CancellationToken cancellationToken)
    {
        if (!user.EmailConfirmed || await _userManager.IsLockedOutAsync(user))
            throw new UnauthorizedException("Email təsdiqlənməyib və ya hesab müvəqqəti bloklanıb.");
        var roles = await _userManager.GetRolesAsync(user);
        var permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null) continue;

            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims.Where(x => x.Type == CustomClaimTypes.Permission))
            {
                permissions.Add(claim.Value);
            }
        }

        var accessToken = _tokenService.CreateAccessToken(
            new TokenUser(user.Id, user.Email!, user.Fullname, roles.ToArray(), permissions.ToArray()));

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
        var tokenHash = _tokenService.ComputeRefreshTokenHash(request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

        var now = DateTime.UtcNow;
        if (storedToken is null || storedToken.RevokedAtUtc is not null || storedToken.ExpiresAtUtc <= now)
        {
            throw new UnauthorizedException("Refresh token is invalid or expired.");
        }
        if (!storedToken.User.EmailConfirmed)
        {
            throw new UnauthorizedException(
                "Please verify your email before accessing your account.");
        }

        if (await _userManager.IsLockedOutAsync(storedToken.User))
            throw new UnauthorizedException("The account is temporarily locked.");
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        var consumed = await _dbContext.RefreshTokens
            .Where(x => x.Id == storedToken.Id && x.RevokedAtUtc == null && x.ExpiresAtUtc > now)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.RevokedAtUtc, now), cancellationToken);
        if (consumed != 1)
            throw new UnauthorizedException("Refresh token artıq istifadə olunub.");
        var response = await CreateAuthResponseAsync(storedToken.User, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return response;
    }

    public async Task LogoutAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var tokenHash = _tokenService.ComputeRefreshTokenHash(request.RefreshToken);

        var storedToken = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

        if (storedToken is null || storedToken.RevokedAtUtc is not null)
        {
            return;
        }

        storedToken.RevokedAtUtc = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ResendVerificationAsync(string email, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(email.Trim());
        if (user is null || user.EmailConfirmed) return;
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailService.SendVerificationEmailAsync(user.Email!, user.Id, token);
    }

    public async Task VerifyEmailAsync(string userId, string token, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new ValidationException("İstifadəçi tapılmadı.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            throw new ValidationException("Təsdiqləmə linki səhvdir və ya vaxtı keçib.");
        }
    }
}
