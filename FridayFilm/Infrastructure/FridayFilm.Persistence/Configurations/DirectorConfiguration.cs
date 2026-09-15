using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using FridayFilm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FridayFilm.Persistence.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(d => d.Slug).IsUnique();

        builder.Property(d => d.Nationality)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Bio)
            .HasMaxLength(2000);

        // DÜZƏLDİLMİŞ ƏLAQƏ HİSSƏSİ
        builder.HasOne(d => d.Image)
               .WithOne(i => i.Director) // <--- Əlaqə bərpa olundu
               .HasForeignKey<Director>(d => d.ImageId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new Director
            {
                Id = Guid.Parse("22222222-3333-4444-5555-666666666601"),
                FullName = "Christopher Nolan",
                Slug = "christopher-nolan",
                Nationality = "British-American",
                Gender = Gender.Male,
                Bio = "Known for complex narratives like Inception, Interstellar, and Oppenheimer."
            },
            new Director
            {
                Id = Guid.Parse("22222222-3333-4444-5555-666666666602"),
                FullName = "Quentin Tarantino",
                Slug = "quentin-tarantino",
                Nationality = "American",
                Gender = Gender.Male,
                Bio = "Famous for non-linear storylines and stylized violence in films like Pulp Fiction."
            },
            new Director
            {
                Id = Guid.Parse("22222222-3333-4444-5555-666666666603"),
                FullName = "Greta Gerwig",
                Slug = "greta-gerwig",
                Nationality = "American",
                Gender = Gender.Female,
                Bio = "Acclaimed director of Lady Bird, Little Women, and Barbie."
            },
            new Director
            {
                Id = SeedIds.Directors.LanaWachowski,
                FullName = "Lana Wachowski",
                Slug = "lana-wachowski",
                Nationality = "American",
                Gender = Gender.Female,
                Bio = "Co-creator of The Matrix, a landmark of science fiction cinema.",
                CreatedDate = SeedIds.SeedDate
            },
            new Director
            {
                Id = SeedIds.Directors.GeorgeMiller,
                FullName = "George Miller",
                Slug = "george-miller",
                Nationality = "Australian",
                Gender = Gender.Male,
                Bio = "Creator of the Mad Max franchise, known for practical action filmmaking.",
                CreatedDate = SeedIds.SeedDate
            },
            new Director
            {
                Id = SeedIds.Directors.DarrenAronofsky,
                FullName = "Darren Aronofsky",
                Slug = "darren-aronofsky",
                Nationality = "American",
                Gender = Gender.Male,
                Bio = "Known for psychologically intense films such as Black Swan and Requiem for a Dream.",
                CreatedDate = SeedIds.SeedDate
            },
            new Director
            {
                Id = SeedIds.Directors.MartinScorsese,
                FullName = "Martin Scorsese",
                Slug = "martin-scorsese",
                Nationality = "American",
                Gender = Gender.Male,
                Bio = "One of the most influential directors in cinema history, famous for crime dramas.",
                CreatedDate = SeedIds.SeedDate
            },
            new Director
            {
                Id = SeedIds.Directors.DenisVilleneuve,
                FullName = "Denis Villeneuve",
                Slug = "denis-villeneuve",
                Nationality = "Canadian",
                Gender = Gender.Male,
                Bio = "Acclaimed for atmospheric science fiction including Arrival, Blade Runner 2049 and Dune.",
                CreatedDate = SeedIds.SeedDate
            }
        );
    }
}