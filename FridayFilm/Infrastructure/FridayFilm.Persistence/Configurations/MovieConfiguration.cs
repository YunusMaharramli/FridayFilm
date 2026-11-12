using FridayFilm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridayFilm.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {



        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(250);

      
        builder.Property(m => m.IMDB)
            .HasPrecision(3, 1)
            .IsRequired();

    
        builder.Property(m => m.CoverImg)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(m => m.Year)
            .IsRequired();

        builder.HasOne(m => m.MovieDetail)
               .WithOne(md => md.Movie)
               .HasForeignKey<MovieDetail>(md => md.Id);

     
        builder.HasOne(m => m.Language)
               .WithMany(l => l.Movies)
               .HasForeignKey(m => m.LanguageId);

  
        builder.HasMany(m => m.Images)
               .WithOne(i => i.Movie)
               .HasForeignKey(i => i.MovieId);


        builder.HasMany(m => m.Directors)
               .WithMany(d => d.Movies);

        
        builder.HasMany(m => m.Actors)
               .WithMany(a => a.Movies);
        builder.HasQueryFilter(x => !x.IsDeleted);


    }
}