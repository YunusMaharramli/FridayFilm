using FridayFilm.Domain.Entities;
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
            }
        );
    }
}