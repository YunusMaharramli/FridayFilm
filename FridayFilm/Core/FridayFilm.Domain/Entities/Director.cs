using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities
{
    public class Director : BaseEntity
    {
        public string Fullname { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public List<Movie> Movies { get; set; } = new();

      
      
    }
}