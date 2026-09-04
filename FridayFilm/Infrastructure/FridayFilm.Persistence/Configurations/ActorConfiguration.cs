using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FridayFilm.Persistence.Configurations;

public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.Nationality)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(a => a.Slug).IsUnique();

        builder.Property(a => a.Nickname)
            .HasMaxLength(100);

        builder.Property(a => a.Bio)
            .HasMaxLength(2000);

        builder.Property(a => a.Gender)
           .HasDefaultValue(Gender.Other);

        // DÜZƏLDİLMİŞ ƏLAQƏ HİSSƏSİ
        builder.HasOne(a => a.Image)
               .WithOne(i => i.Actor)
               .HasForeignKey<Actor>(a => a.ImageId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555501"),
                FullName = "Leonardo DiCaprio",
                Slug = "leonardo-dicaprio",
                Nationality = "American",
                Gender = Gender.Male,
                Nickname = "Leo",
                Bio = "Academy Award-winning actor known for Titanic, Inception, and The Revenant."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555502"),
                FullName = "Scarlett Johansson",
                Slug = "scarlett-johansson",
                Nationality = "American",
                Gender = Gender.Female,
                Nickname = "ScarJo",
                Bio = "Highly paid actress globally, known for her role as Black Widow in the MCU."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555503"),
                FullName = "Cillian Murphy",
                Slug = "cillian-murphy",
                Nationality = "Irish",
                Gender = Gender.Male,
                Nickname = "Tommy",
                Bio = "Acclaimed for his roles in Peaky Blinders and Christopher Nolan's Oppenheimer."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555504"),
                FullName = "Margot Robbie",
                Slug = "margot-robbie",
                Nationality = "Australian",
                Gender = Gender.Female,
                Nickname = "Magot",
                Bio = "Known for blockbuster hits like The Wolf of Wall Street and Barbie."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555505"),
                FullName = "Tom Hardy",
                Slug = "tom-hardy",
                Nationality = "British",
                Gender = Gender.Male,
                Nickname = null,
                Bio = "Versatile actor famous for Mad Max: Fury Road, Venom, and The Dark Knight Rises."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555506"),
                FullName = "Meryl Streep",
                Slug = "meryl-streep",
                Nationality = "American",
                Gender = Gender.Female,
                Nickname = null,
                Bio = "Often described as the best actress of her generation, holding a record number of Academy Award nominations."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555507"),
                FullName = "Keanu Reeves",
                Slug = "keanu-reeves",
                Nationality = "Canadian",
                Gender = Gender.Male,
                Nickname = "The One",
                Bio = "Beloved action star of The Matrix and John Wick franchises."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555508"),
                FullName = "Natalie Portman",
                Slug = "natalie-portman",
                Nationality = "Israeli/American",
                Gender = Gender.Female,
                Nickname = "Nat",
                Bio = "Oscar winner for Black Swan and famous for her role in Star Wars."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555509"),
                FullName = "Christian Bale",
                Slug = "christian-bale",
                Nationality = "British",
                Gender = Gender.Male,
                Nickname = null,
                Bio = "Known for his intense method acting and physical transformations for roles."
            },
            new Actor
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555510"),
                FullName = "Charlize Theron",
                Slug = "charlize-theron",
                Nationality = "South African",
                Gender = Gender.Female,
                Nickname = null,
                Bio = "Critically acclaimed star of Monster and Mad Max: Fury Road."
            }
        );
    }
}