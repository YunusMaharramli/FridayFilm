using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;

namespace FridayFilm.Domain.Entities;

public class Language : BaseEntity
{
    public Lang Lang { get; set; }
    public List<Movie> Movies { get; set; } = new();


  
}