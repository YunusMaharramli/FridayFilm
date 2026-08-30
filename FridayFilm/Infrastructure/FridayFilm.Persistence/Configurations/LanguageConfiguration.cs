using FridayFilm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FridayFilm.Persistence.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
     

        // Enum tipi olduğu üçün onsuz da boş qala bilməz (nullable deyil), 
        // amma yenə də qayda olaraq açıq-aydın qeyd edirik:
        builder.Property(l => l.Lang)
            .IsRequired();
    }
}