using FridayFilm.Domain.Entities;
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
           
    }
}