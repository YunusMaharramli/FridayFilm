using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}