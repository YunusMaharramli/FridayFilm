using System;
using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;

namespace FridayFilm.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal IMDB { get; set; }
        public int Year { get; set; }
        public string CoverImg { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int RateCount { get; set; }

        public Lang Language { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}