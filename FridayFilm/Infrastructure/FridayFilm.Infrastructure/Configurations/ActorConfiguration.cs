using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums; // Gender enum-u tanıması üçün bu using əlavə olunmalıdır
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Infrastructure.Configurations;

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

        builder.Property(a => a.Nickname)
            .HasMaxLength(100);

        builder.Property(a => a.Bio)
            .HasMaxLength(2000);

    
    }
}