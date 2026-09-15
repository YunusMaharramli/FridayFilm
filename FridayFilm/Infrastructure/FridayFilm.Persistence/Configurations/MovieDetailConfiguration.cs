using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FridayFilm.Persistence.Configurations;

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
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasData(
            new MovieDetail
            {
                Id = SeedIds.Movies.Inception,
                Description = "Dom Cobb yuxu içində məlumat oğurlayan usta bir oğrudur. Ona son bir iş təklif olunur: oğurluq yox, fikir əkmək — inception. Ailəsinə qovuşmaq üçün komandası ilə şüurun ən dərin qatlarına enir.",
                TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.TheDarkKnight,
                Description = "Joker Qothem şəhərini xaosa sürükləyəndə Batman, Komissar Gordon və prokuror Harvey Dent birləşir. Lakin Jokerin əsl silahı zorakılıq deyil — insanların bir-birinə olan inamını qırmaqdır.",
                TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.Oppenheimer,
                Description = "J. Robert Oppenheimer Manhattan layihəsinə rəhbərlik edərək atom bombasını yaradır. Zəfərdən sonra isə yaratdığı silahın nəticələri və vicdan əzabı onu izləməyə başlayır.",
                TrailerUrl = "https://www.youtube.com/watch?v=uYPbbksJxIg",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.Interstellar,
                Description = "Yer üzü yaşayış üçün yararsızlaşdıqca bir qrup tədqiqatçı qurd dəliyindən keçərək bəşəriyyətə yeni ev axtarır. Zaman, cazibə və ata-övlad sevgisi arasında çətin seçim onları gözləyir.",
                TrailerUrl = "https://www.youtube.com/watch?v=zSWdZVtXT7E",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.PulpFiction,
                Description = "İki muzdlu qatil, bir boksçu, cinayət aləminin patronu və onun arvadı — hekayələri gözlənilməz şəkildə kəsişir. Qeyri-xətti quruluşu ilə müasir kinonun gedişatını dəyişən əsər.",
                TrailerUrl = "https://www.youtube.com/watch?v=s7EdQ4FqbhY",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.DjangoUnchained,
                Description = "Azadlığa buraxılmış qul Django, mükafat ovçusu Dr. Schultz ilə birləşərək arvadını qəddar plantasiya sahibinin əlindən xilas etməyə çalışır.",
                TrailerUrl = "https://www.youtube.com/watch?v=0fUCuvNlOCg",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.Barbie,
                Description = "Barbie mükəmməl Barbieland dünyasını tərk edib real dünyaya çıxır. Kimlik, gözləntilər və özünü tapmaq haqqında rəngarəng və kinayəli bir səyahət başlayır.",
                TrailerUrl = "https://www.youtube.com/watch?v=pBk4NYhWNMM",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.LittleWomen,
                Description = "March bacılarının böyümə hekayəsi — sevgi, itki, yaradıcılıq və müstəqillik arasında öz yollarını axtaran dörd qadının portreti.",
                TrailerUrl = "https://www.youtube.com/watch?v=AST2-4db4ic",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.TheMatrix,
                Description = "Proqramçı Neo yaşadığı dünyanın simulyasiya olduğunu öyrənir. Morpheus və Trinity ilə birlikdə maşınların qurduğu Matrisə qarşı üsyana qoşulur.",
                TrailerUrl = "https://www.youtube.com/watch?v=vKQi3bBA1y8",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.MadMaxFuryRoad,
                Description = "Post-apokaliptik səhrada Max və Furiosa zalım Immortan Joe-dan qaçan qadınları xilas etmək üçün dayanmadan davam edən vəhşi bir təqib yarışına girir.",
                TrailerUrl = "https://www.youtube.com/watch?v=hEJnMQG9ev8",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.BlackSwan,
                Description = "Balerina Nina Qu Gölü tamaşasında baş rola seçilir. Mükəmməllik axtarışı və rəqabət onu tədricən öz şüurunun qaranlıq tərəfinə sürükləyir.",
                TrailerUrl = "https://www.youtube.com/watch?v=5jaI1XOB-bs",
                CreatedDate = SeedIds.SeedDate
            },
            new MovieDetail
            {
                Id = SeedIds.Movies.TheWolfOfWallStreet,
                Description = "Jordan Belfort-un Wall Street-də sıfırdan sərvətə, oradan da süquta gedən yolu. Xəyanət, həddindən artıq israf və FBI təqibi ilə dolu həqiqi hekayə.",
                TrailerUrl = "https://www.youtube.com/watch?v=iszwuX1AK6A",
                CreatedDate = SeedIds.SeedDate
            }
        );

    }
}