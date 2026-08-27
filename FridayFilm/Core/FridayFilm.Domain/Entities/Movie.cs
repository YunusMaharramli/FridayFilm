using System;
using FridayFilm.Domain.Common;
using FridayFilm.Domain.Enums;

namespace FridayFilm.Domain.Entities;

public class Movie : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal IMDB { get; set; }
    public int Year { get; set; }
    public string CoverImg { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public int RateCount { get; set; }

    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public MovieDetail? MovieDetail { get; set; }
    public Category Category { get; set; } = null!;
    public List<Genre> Genres { get; set; } = new();
    public  List<Director> Directors { get; set; }=new();
    public List<Actor> Actors { get; set; }= new();
    public List<FilmImage> Images { get; set; } = new();

 

}