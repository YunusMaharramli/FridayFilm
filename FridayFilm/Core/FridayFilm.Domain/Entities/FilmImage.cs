using System;
using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities
{
    public class FilmImage : BaseEntity
    {
        public string PhotoUrl { get; set; } = string.Empty;

        public Bio? Bio { get; set; }

        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }

      
        public Actor? Actor { get; set; }
        public Director? Director { get; set; }
    }
}