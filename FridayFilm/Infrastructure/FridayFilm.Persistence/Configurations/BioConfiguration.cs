using FridayFilm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FridayFilm.Persistence.Configurations; // Qovluq yolunu layihənə görə tənzimləyərsən

public class BioConfiguration : IEntityTypeConfiguration<Bio>
{
    public void Configure(EntityTypeBuilder<Bio> builder)
    {
        // Əlaqə tam düzgündür
        builder.HasOne(b => b.Logo)
               .WithOne(f => f.Bio)
               .HasForeignKey<Bio>(b => b.LogoId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(
            new Bio
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-123456789abc"),
                Description = "FridayFilm - Dünyanın ən yaxşı filmlərini kəşf etmək üçün ideal platforma.",
                ContactPhone = "+994 50 123 45 67",
                ContactEmail = "info@fridayfilm.com",
                InstagramUrl = "https://instagram.com/fridayfilm",
                FacebookUrl = "https://facebook.com/fridayfilm",
                TwitterUrl = "https://twitter.com/fridayfilm",
                LogoId = null
            }
        );
    }
}