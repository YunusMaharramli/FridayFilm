using FridayFilm.Application.Dtos.RoleDtos;
namespace FridayFilm.Application.Abstracts.Services;

public interface IRoleService
{
    Task<IReadOnlyCollection<RoleResponse>> GetAllAsync();

    Task CreateAsync(CreateRoleRequest request);

    Task UpdateAsync(
        string roleId,
        UpdateRoleRequest request);
}