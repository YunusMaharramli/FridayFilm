using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;

namespace FridayFilm.Domain.Entities
{
    public class Director : BaseEntity
    {
        public string Fullname { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public Gender Genre { get; set; }
        public List<Movie> Movies { get; set; } = new();

      
      
    }
}