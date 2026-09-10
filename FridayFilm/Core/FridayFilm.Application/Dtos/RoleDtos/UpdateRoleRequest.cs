namespace FridayFilm.Application.Dtos.RoleDtos;
public sealed record UpdateRoleRequest(
    string Name,
    IReadOnlyCollection<string> Permissions);