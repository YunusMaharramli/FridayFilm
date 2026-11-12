using FridayFilm.Domain.Entities;
using FridayFilm.Domain.Enums;
using FridayFilm.Persistence.Seed;
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
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new Language { Id = SeedIds.Languages.Azerbaijani, Lang = Lang.Azerbaijani, CreatedDate = SeedIds.SeedDate },
            new Language { Id = SeedIds.Languages.English, Lang = Lang.English, CreatedDate = SeedIds.SeedDate },
            new Language { Id = SeedIds.Languages.Russian, Lang = Lang.Russian, CreatedDate = SeedIds.SeedDate },
            new Language { Id = SeedIds.Languages.Turkish, Lang = Lang.Turkish, CreatedDate = SeedIds.SeedDate }
        );
    }
}