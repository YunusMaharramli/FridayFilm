using FridayFilm.Domain.Entities;
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
    }
}