using FridayFilm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Infrastructure.Configurations;

public class MovieDetailConfiguration : IEntityTypeConfiguration<MovieDetail>
{
    public void Configure(EntityTypeBuilder<MovieDetail> builder)
    {
      
        builder.Property(md => md.Description)
            .IsRequired()
            .HasMaxLength(2000);

   
        builder.Property(md => md.TrailerUrl)
            .IsRequired()
            .HasMaxLength(500);

    }
}