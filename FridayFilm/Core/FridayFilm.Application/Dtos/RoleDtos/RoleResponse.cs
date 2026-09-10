namespace FridayFilm.Application.Dtos.RoleDtos;
public sealed record RoleResponse(
    string Id,
    string Name,
    IReadOnlyCollection<string> Permissions);