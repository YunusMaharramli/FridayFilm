using System.Security.Claims;
using FridayFilm.Application.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Persistence.Seed;

public static class UserRoleSeeder
{
    private static readonly string[] DefaultUserPermissions =
    [
        Permissions.Actors.Read,
        Permissions.Directors.Read,
        Permissions.Categories.Read,
        Permissions.Genres.Read,
        Permissions.Bios.Read,
        Permissions.MovieDetails.Read,
        Permissions.Images.Read
    ];

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var userRole = await roleManager.FindByNameAsync(AppRoles.User);

        if (userRole is null)
        {
            userRole = new IdentityRole(AppRoles.User);
            var createResult = await roleManager.CreateAsync(userRole);
            EnsureSucceeded(createResult);
        }

        var existingClaims = await roleManager.GetClaimsAsync(userRole);
        var existingPermissions = existingClaims
            .Where(x => x.Type == CustomClaimTypes.Permission)
            .Select(x => x.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var permission in DefaultUserPermissions)
        {
            if (existingPermissions.Contains(permission))
                continue;

            var result = await roleManager.AddClaimAsync(
                userRole,
                new Claim(CustomClaimTypes.Permission, permission));

            EnsureSucceeded(result);
        }
    }

    private static void EnsureSucceeded(IdentityResult result)
    {
        if (result.Succeeded) return;
        throw new InvalidOperationException(
            string.Join(" ", result.Errors.Select(x => x.Description)));
    }
}
