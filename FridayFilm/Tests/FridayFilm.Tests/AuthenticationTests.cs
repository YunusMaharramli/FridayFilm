using FridayFilm.Application.Abstracts.Notifications;
using FridayFilm.Application.Dtos.AuthDtos;
using FridayFilm.Application.Exceptions;
using FridayFilm.Persistence.Contexts;
using FridayFilm.Persistence.Services;
using FridayFilm.Persistence.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace FridayFilm.Tests;

public class AuthenticationTests
{
    [Fact]
    public async Task UnconfirmedUserCannotLogin()
    {
        using var db = Database();
        using var users = new Users(new ApplicationUser { EmailConfirmed = false });
        var service = Service(users, new Email(), db);
        await Assert.ThrowsAsync<UnauthorizedException>(() => service.LoginAsync(new LoginRequest("test@example.invalid", "Test-only!123")));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task ResendDoesNotSendToUnknownOrAlreadyConfirmedAccounts(bool exists)
    {
        using var db = Database();
        using var users = new Users(exists ? new ApplicationUser { EmailConfirmed = true } : null);
        var email = new Email();
        await Service(users, email, db).ResendVerificationAsync("test@example.invalid");
        Assert.Equal(0, email.Sent);
    }

    [Fact]
    public async Task InvalidConfirmationTokenIsRejected()
    {
        using var db = Database();
        using var users = new Users(new ApplicationUser { Id = "test" });
        await Assert.ThrowsAsync<ValidationException>(() => Service(users, new Email(), db).VerifyEmailAsync("test", "invalid"));
    }

    private static FridayFilmDbContext Database() => new(new DbContextOptionsBuilder<FridayFilmDbContext>()
        .UseNpgsql("Host=localhost;Database=not_used;Username=not_used;Password=not_used").Options);
    private static AuthenticationService Service(Users users, Email email, FridayFilmDbContext db) =>
        new(users, null!, null!, null!, email, db, NullLogger<AuthenticationService>.Instance);

    private sealed class Email : IEmailService
    {
        public int Sent { get; private set; }
        public Task SendVerificationEmailAsync(string toEmail, string userId, string token) { Sent++; return Task.CompletedTask; }
    }

    private sealed class Users(ApplicationUser? user) : UserManager<ApplicationUser>(
        new Store(), Microsoft.Extensions.Options.Options.Create(new IdentityOptions()), new PasswordHasher<ApplicationUser>(),
        [], [], new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null!, NullLogger<UserManager<ApplicationUser>>.Instance)
    {
        public override Task<ApplicationUser?> FindByEmailAsync(string email) => Task.FromResult(user);
        public override Task<ApplicationUser?> FindByIdAsync(string id) => Task.FromResult(user);
        public override Task<IdentityResult> ConfirmEmailAsync(ApplicationUser account, string token) =>
            Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Invalid token" }));
    }

    private sealed class Store : IUserStore<ApplicationUser>
    {
        public void Dispose() { }
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken token) => Task.FromResult(user.Id);
        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken token) => Task.FromResult(user.UserName);
        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken token) => Task.FromResult(user.NormalizedUserName);
        public Task SetUserNameAsync(ApplicationUser user, string? name, CancellationToken token) { user.UserName = name; return Task.CompletedTask; }
        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? name, CancellationToken token) { user.NormalizedUserName = name; return Task.CompletedTask; }
        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken token) => throw new NotSupportedException();
        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken token) => throw new NotSupportedException();
        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken token) => throw new NotSupportedException();
        public Task<ApplicationUser?> FindByIdAsync(string id, CancellationToken token) => throw new NotSupportedException();
        public Task<ApplicationUser?> FindByNameAsync(string name, CancellationToken token) => throw new NotSupportedException();
    }
}
