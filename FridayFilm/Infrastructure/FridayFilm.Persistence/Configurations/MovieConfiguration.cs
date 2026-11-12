using System;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {



        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(250);

      
        builder.Property(m => m.IMDB)
            .HasPrecision(3, 1)
            .IsRequired();

    
        builder.Property(m => m.CoverImg)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Year)
            .IsRequired();

        builder.HasOne(m => m.MovieDetail)
               .WithOne(md => md.Movie)
               .HasForeignKey<MovieDetail>(md => md.Id);

     
        builder.HasOne(m => m.Language)
               .WithMany(l => l.Movies)
               .HasForeignKey(m => m.LanguageId);

  
        builder.HasMany(m => m.Images)
               .WithOne(i => i.Movie)
               .HasForeignKey(i => i.MovieId);


        builder.HasMany(m => m.Directors)
               .WithMany(d => d.Movies);

        
        builder.HasMany(m => m.Actors)
               .WithMany(a => a.Movies);
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new Movie
            {
                Id = SeedIds.Movies.Inception,
                Name = "Inception",
                IMDB = 8.8m,
                Year = 2010,
                Duration = new TimeSpan(2, 28, 0),
                RateCount = 2400000,
                CoverImg = "/images/movies/inception.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.ScienceFiction,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.TheDarkKnight,
                Name = "The Dark Knight",
                IMDB = 9.0m,
                Year = 2008,
                Duration = new TimeSpan(2, 32, 0),
                RateCount = 2900000,
                CoverImg = "/images/movies/the-dark-knight.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Action,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.Oppenheimer,
                Name = "Oppenheimer",
                IMDB = 8.3m,
                Year = 2023,
                Duration = new TimeSpan(3, 0, 0),
                RateCount = 820000,
                CoverImg = "/images/movies/oppenheimer.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Historical,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.Interstellar,
                Name = "Interstellar",
                IMDB = 8.7m,
                Year = 2014,
                Duration = new TimeSpan(2, 49, 0),
                RateCount = 2100000,
                CoverImg = "/images/movies/interstellar.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.ScienceFiction,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.PulpFiction,
                Name = "Pulp Fiction",
                IMDB = 8.9m,
                Year = 1994,
                Duration = new TimeSpan(2, 34, 0),
                RateCount = 2200000,
                CoverImg = "/images/movies/pulp-fiction.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Crime,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.DjangoUnchained,
                Name = "Django Unchained",
                IMDB = 8.4m,
                Year = 2012,
                Duration = new TimeSpan(2, 45, 0),
                RateCount = 1700000,
                CoverImg = "/images/movies/django-unchained.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Crime,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.Barbie,
                Name = "Barbie",
                IMDB = 6.8m,
                Year = 2023,
                Duration = new TimeSpan(1, 54, 0),
                RateCount = 580000,
                CoverImg = "/images/movies/barbie.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Comedy,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.LittleWomen,
                Name = "Little Women",
                IMDB = 7.8m,
                Year = 2019,
                Duration = new TimeSpan(2, 15, 0),
                RateCount = 230000,
                CoverImg = "/images/movies/little-women.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Drama,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.TheMatrix,
                Name = "The Matrix",
                IMDB = 8.7m,
                Year = 1999,
                Duration = new TimeSpan(2, 16, 0),
                RateCount = 2000000,
                CoverImg = "/images/movies/the-matrix.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.ScienceFiction,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.MadMaxFuryRoad,
                Name = "Mad Max: Fury Road",
                IMDB = 8.1m,
                Year = 2015,
                Duration = new TimeSpan(2, 0, 0),
                RateCount = 1100000,
                CoverImg = "/images/movies/mad-max-fury-road.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Action,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.BlackSwan,
                Name = "Black Swan",
                IMDB = 8.0m,
                Year = 2010,
                Duration = new TimeSpan(1, 48, 0),
                RateCount = 750000,
                CoverImg = "/images/movies/black-swan.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Thriller,
                CreatedDate = SeedIds.SeedDate
            },
            new Movie
            {
                Id = SeedIds.Movies.TheWolfOfWallStreet,
                Name = "The Wolf of Wall Street",
                IMDB = 8.2m,
                Year = 2013,
                Duration = new TimeSpan(3, 0, 0),
                RateCount = 1500000,
                CoverImg = "/images/movies/the-wolf-of-wall-street.jpg",
                LanguageId = SeedIds.Languages.English,
                CategoryId = SeedIds.Categories.Crime,
                CreatedDate = SeedIds.SeedDate
            }
        );


    }
}