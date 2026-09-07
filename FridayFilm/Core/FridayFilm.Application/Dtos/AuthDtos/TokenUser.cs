namespace FridayFilm.Application.Dtos.AuthDtos;

public sealed record TokenUser(
    string Id,
    string Email,
    string Name,
    IReadOnlyCollection<string> Roles);
