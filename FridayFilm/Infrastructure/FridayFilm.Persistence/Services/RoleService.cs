using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Dtos.RoleDtos;
using FridayFilm.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FridayFilm.Persistence.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IReadOnlyCollection<RoleResponse>> GetAllAsync()
    {
        var roles = await _roleManager.Roles
            .AsNoTracking()
            .ToListAsync();

        var response = new List<RoleResponse>();

        foreach (var role in roles)
        {
            var claims =
                await _roleManager.GetClaimsAsync(role);

            var permissions = claims
                .Where(x =>
                    x.Type == CustomClaimTypes.Permission)
                .Select(x => x.Value)
                .ToArray();

            response.Add(new RoleResponse(
                role.Id,
                role.Name!,
                permissions));
        }

        return response;
    }

    public async Task CreateAsync(CreateRoleRequest request)
    {
        var roleName = request.Name.Trim();

        if (await _roleManager.RoleExistsAsync(roleName))
        {
            throw new ConflictException(
                "Bu adda rol artıq mövcuddur.");
        }

        var permissions =
            ValidatePermissions(request.Permissions);

        var role = new IdentityRole(roleName);

        var createResult =
            await _roleManager.CreateAsync(role);

        EnsureSucceeded(createResult);

        foreach (var permission in permissions)
        {
            var result =
                await _roleManager.AddClaimAsync(
                    role,
                    new Claim(
                        CustomClaimTypes.Permission,
                        permission));

            if (!result.Succeeded)
            {
                await _roleManager.DeleteAsync(role);
                EnsureSucceeded(result);
            }
        }
    }

    public async Task UpdateAsync(
        string roleId,
        UpdateRoleRequest request)
    {
        var role =
            await _roleManager.FindByIdAsync(roleId)
            ?? throw new NotFoundException(
                "Rol tapılmadı.");

        var roleName = request.Name.Trim();

        var roleWithSameName =
            await _roleManager.FindByNameAsync(roleName);

        if (roleWithSameName is not null &&
            roleWithSameName.Id != role.Id)
        {
            throw new ConflictException(
                "Bu adda başqa rol artıq mövcuddur.");
        }

        var requestedPermissions =
            ValidatePermissions(request.Permissions);

        role.Name = roleName;

        var updateResult =
            await _roleManager.UpdateAsync(role);

        EnsureSucceeded(updateResult);

        await SynchronizePermissionsAsync(
            role,
            requestedPermissions);
    }

    private async Task SynchronizePermissionsAsync(
        IdentityRole role,
        IReadOnlyCollection<string> requestedPermissions)
    {
        var claims =
            await _roleManager.GetClaimsAsync(role);

        var currentPermissionClaims = claims
            .Where(x =>
                x.Type == CustomClaimTypes.Permission)
            .ToArray();

        var requestedPermissionSet =
            requestedPermissions.ToHashSet(
                StringComparer.OrdinalIgnoreCase);

        foreach (var claim in currentPermissionClaims)
        {
            if (requestedPermissionSet.Contains(claim.Value))
            {
                continue;
            }

            var result =
                await _roleManager.RemoveClaimAsync(
                    role,
                    claim);

            EnsureSucceeded(result);
        }

        var currentPermissionSet =
            currentPermissionClaims
                .Select(x => x.Value)
                .ToHashSet(
                    StringComparer.OrdinalIgnoreCase);

        foreach (var permission in requestedPermissions)
        {
            if (currentPermissionSet.Contains(permission))
            {
                continue;
            }

            var result =
                await _roleManager.AddClaimAsync(
                    role,
                    new Claim(
                        CustomClaimTypes.Permission,
                        permission));

            EnsureSucceeded(result);
        }
    }

    private static IReadOnlyCollection<string>
        ValidatePermissions(
            IReadOnlyCollection<string> permissions)
    {
        var result = permissions
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var invalidPermissions = result
            .Where(x => !Permissions.IsValid(x))
            .ToArray();

        if (invalidPermissions.Length > 0)
        {
            throw new ValidationException(
                $"Mövcud olmayan permission: " +
                $"{string.Join(", ", invalidPermissions)}");
        }

        return result;
    }

    private static void EnsureSucceeded(
        IdentityResult result)
    {
        if (result.Succeeded)
        {
            return;
        }

        var errors = string.Join(
            " ",
            result.Errors.Select(x => x.Description));

        throw new ValidationException(errors);
    }
}
