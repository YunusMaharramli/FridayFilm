using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
      

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(g => g.Movies)
               .WithMany(m => m.Genres);
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new Genre { Id = SeedIds.Genres.Action, Name = "Action", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Adventure, Name = "Adventure", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.ScienceFiction, Name = "Science Fiction", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Drama, Name = "Drama", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Thriller, Name = "Thriller", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Crime, Name = "Crime", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Comedy, Name = "Comedy", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Romance, Name = "Romance", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Mystery, Name = "Mystery", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Fantasy, Name = "Fantasy", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Historical, Name = "Historical", CreatedDate = SeedIds.SeedDate },
            new Genre { Id = SeedIds.Genres.Biography, Name = "Biography", CreatedDate = SeedIds.SeedDate }
        );

    }
}