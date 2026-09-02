using System;
using FridayFilm.Domain.Common;

namespace FridayFilm.Domain.Entities
{
    public class Bio : BaseEntity
    {
        public string? Description { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }

        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }

        public Guid? LogoId { get; set; }
        public FilmImage? Logo { get; set; }
    }
}