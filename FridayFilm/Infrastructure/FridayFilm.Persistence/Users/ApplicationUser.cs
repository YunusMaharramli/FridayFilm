using Microsoft.AspNetCore.Identity;

namespace FridayFilm.Persistence.Users;

public class ApplicationUser:IdentityUser
{
    public string Fullname { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}
