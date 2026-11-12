using FridayFilm.Domain.Common;

namespace FridayFilm.Persistence.Users;

public sealed class RefreshToken : BaseEntity
{
    public string TokenHash { get; set; } = null!;

    public DateTime ExpiresAtUtc { get; set; }

    public DateTime? RevokedAtUtc { get; set; }

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;
}