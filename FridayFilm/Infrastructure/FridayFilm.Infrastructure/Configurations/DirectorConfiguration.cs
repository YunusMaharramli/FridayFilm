using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Infrastructure.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {


        builder.Property(d => d.Fullname)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(d => d.Nationality)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Bio)
            .HasMaxLength(2000);

     
    }
}