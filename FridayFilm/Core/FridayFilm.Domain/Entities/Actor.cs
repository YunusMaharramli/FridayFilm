using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;
using System.Security.Principal;

namespace FridayFilm.Domain.Entities
{
    public class Actor : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Slug { get; set; }
        public Gender Gender { get; set; }
        public string? Nickname { get; set; }
        public string? Bio { get; set; }
        public List<Movie> Movies { get; set; } = new();

        public Guid? ImageId { get; set; }
        public FilmImage? Image { get; set; }
    }
}