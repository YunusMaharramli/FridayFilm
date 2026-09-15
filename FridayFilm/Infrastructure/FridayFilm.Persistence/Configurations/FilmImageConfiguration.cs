using System;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Persistence.Configurations;

public class FilmImageConfiguration : IEntityTypeConfiguration<FilmImage>
{
    public void Configure(EntityTypeBuilder<FilmImage> builder)
    {
        
        builder.Property(f => f.PhotoUrl)
            .IsRequired()
            .HasMaxLength(500);
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000001"),
                PhotoUrl = "/images/movies/inception-1.jpg",
                MovieId = SeedIds.Movies.Inception,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000002"),
                PhotoUrl = "/images/movies/inception-2.jpg",
                MovieId = SeedIds.Movies.Inception,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000003"),
                PhotoUrl = "/images/movies/the-dark-knight-1.jpg",
                MovieId = SeedIds.Movies.TheDarkKnight,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000004"),
                PhotoUrl = "/images/movies/the-dark-knight-2.jpg",
                MovieId = SeedIds.Movies.TheDarkKnight,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000005"),
                PhotoUrl = "/images/movies/oppenheimer-1.jpg",
                MovieId = SeedIds.Movies.Oppenheimer,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000006"),
                PhotoUrl = "/images/movies/oppenheimer-2.jpg",
                MovieId = SeedIds.Movies.Oppenheimer,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000007"),
                PhotoUrl = "/images/movies/interstellar-1.jpg",
                MovieId = SeedIds.Movies.Interstellar,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000008"),
                PhotoUrl = "/images/movies/interstellar-2.jpg",
                MovieId = SeedIds.Movies.Interstellar,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000009"),
                PhotoUrl = "/images/movies/pulp-fiction-1.jpg",
                MovieId = SeedIds.Movies.PulpFiction,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000010"),
                PhotoUrl = "/images/movies/pulp-fiction-2.jpg",
                MovieId = SeedIds.Movies.PulpFiction,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000011"),
                PhotoUrl = "/images/movies/django-unchained-1.jpg",
                MovieId = SeedIds.Movies.DjangoUnchained,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000012"),
                PhotoUrl = "/images/movies/django-unchained-2.jpg",
                MovieId = SeedIds.Movies.DjangoUnchained,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000013"),
                PhotoUrl = "/images/movies/barbie-1.jpg",
                MovieId = SeedIds.Movies.Barbie,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000014"),
                PhotoUrl = "/images/movies/barbie-2.jpg",
                MovieId = SeedIds.Movies.Barbie,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000015"),
                PhotoUrl = "/images/movies/little-women-1.jpg",
                MovieId = SeedIds.Movies.LittleWomen,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000016"),
                PhotoUrl = "/images/movies/little-women-2.jpg",
                MovieId = SeedIds.Movies.LittleWomen,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000017"),
                PhotoUrl = "/images/movies/the-matrix-1.jpg",
                MovieId = SeedIds.Movies.TheMatrix,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000018"),
                PhotoUrl = "/images/movies/the-matrix-2.jpg",
                MovieId = SeedIds.Movies.TheMatrix,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000019"),
                PhotoUrl = "/images/movies/mad-max-fury-road-1.jpg",
                MovieId = SeedIds.Movies.MadMaxFuryRoad,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000020"),
                PhotoUrl = "/images/movies/mad-max-fury-road-2.jpg",
                MovieId = SeedIds.Movies.MadMaxFuryRoad,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000021"),
                PhotoUrl = "/images/movies/black-swan-1.jpg",
                MovieId = SeedIds.Movies.BlackSwan,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000022"),
                PhotoUrl = "/images/movies/black-swan-2.jpg",
                MovieId = SeedIds.Movies.BlackSwan,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000023"),
                PhotoUrl = "/images/movies/the-wolf-of-wall-street-1.jpg",
                MovieId = SeedIds.Movies.TheWolfOfWallStreet,
                CreatedDate = SeedIds.SeedDate
            },
            new FilmImage
            {
                Id = new Guid("dddd0000-0000-0000-0000-000000000024"),
                PhotoUrl = "/images/movies/the-wolf-of-wall-street-2.jpg",
                MovieId = SeedIds.Movies.TheWolfOfWallStreet,
                CreatedDate = SeedIds.SeedDate
            }
        );
    }
}