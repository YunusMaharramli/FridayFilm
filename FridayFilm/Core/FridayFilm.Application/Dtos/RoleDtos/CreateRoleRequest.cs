namespace FridayFilm.Application.Dtos.RoleDtos;
public sealed record CreateRoleRequest(
    string Name,
    IReadOnlyCollection<string> Permissions);
