using System.Security.Claims;
using FridayFilm.Application.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Persistence.Seed;

public static class AdminRoleSeeder
{
    public static async Task SeedAsync(
        IServiceProvider serviceProvider)
    {
        using var scope =
            serviceProvider.CreateScope();

        var roleManager =
            scope.ServiceProvider
                .GetRequiredService<
                    RoleManager<IdentityRole>>();

        var adminRole =
            await roleManager.FindByNameAsync(
                AppRoles.Admin);

        if (adminRole is null)
        {
            adminRole =
                new IdentityRole(AppRoles.Admin);

            var createResult =
                await roleManager.CreateAsync(adminRole);

            EnsureSucceeded(createResult);
        }

        var existingClaims =
            await roleManager.GetClaimsAsync(adminRole);

        var existingPermissions = existingClaims
            .Where(x =>
                x.Type == CustomClaimTypes.Permission)
            .Select(x => x.Value)
            .ToHashSet(
                StringComparer.OrdinalIgnoreCase);

        foreach (var permission in Permissions.All)
        {
            if (existingPermissions.Contains(permission))
            {
                continue;
            }

            var result =
                await roleManager.AddClaimAsync(
                    adminRole,
                    new Claim(
                        CustomClaimTypes.Permission,
                        permission));

            EnsureSucceeded(result);
        }
    }

    private static void EnsureSucceeded(
        IdentityResult result)
    {
        if (result.Succeeded)
        {
            return;
        }

        throw new InvalidOperationException(
            string.Join(
                " ",
                result.Errors.Select(x => x.Description)));
    }
}