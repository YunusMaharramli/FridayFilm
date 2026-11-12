using FridayFilm.Domain.Common;
using FridayFilm.Domain.Entities;
using FridayFilm.Persistence.Seed;
using FridayFilm.Persistence.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FridayFilm.Persistence.Contexts;

public class FridayFilmDbContext:IdentityDbContext<ApplicationUser>
{
    public FridayFilmDbContext(DbContextOptions<FridayFilmDbContext> options)
        : base(options)
    {
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<FilmImage> FilmImages { get; set; }
    public DbSet<MovieDetail> MovieDetails { get; set; }
    public DbSet<Bio> Bios { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
 
        var seedDate = new DateTime(2026, 8, 31, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Action", Slug = "action", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Comedy", Slug = "comedy", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Drama", Slug = "drama", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Horror", Slug = "horror", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Science Fiction", Slug = "science-fiction", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Romance", Slug = "romance", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Thriller", Slug = "thriller", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Documentary", Slug = "documentary", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Fantasy", Slug = "fantasy", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Animation", Slug = "animation", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Mystery", Slug = "mystery", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Adventure", Slug = "adventure", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Crime", Slug = "crime", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Family", Slug = "family", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Historical", Slug = "historical", CreatedDate = seedDate }
        );

        // Movie <-> Director (DirectorMovie join cedveli)
        modelBuilder.Entity("DirectorMovie").HasData(
            new { DirectorsId = SeedIds.Directors.ChristopherNolan, MoviesId = SeedIds.Movies.Inception },
            new { DirectorsId = SeedIds.Directors.ChristopherNolan, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { DirectorsId = SeedIds.Directors.ChristopherNolan, MoviesId = SeedIds.Movies.Oppenheimer },
            new { DirectorsId = SeedIds.Directors.ChristopherNolan, MoviesId = SeedIds.Movies.Interstellar },
            new { DirectorsId = SeedIds.Directors.QuentinTarantino, MoviesId = SeedIds.Movies.PulpFiction },
            new { DirectorsId = SeedIds.Directors.QuentinTarantino, MoviesId = SeedIds.Movies.DjangoUnchained },
            new { DirectorsId = SeedIds.Directors.GretaGerwig, MoviesId = SeedIds.Movies.Barbie },
            new { DirectorsId = SeedIds.Directors.GretaGerwig, MoviesId = SeedIds.Movies.LittleWomen },
            new { DirectorsId = SeedIds.Directors.LanaWachowski, MoviesId = SeedIds.Movies.TheMatrix },
            new { DirectorsId = SeedIds.Directors.GeorgeMiller, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { DirectorsId = SeedIds.Directors.DarrenAronofsky, MoviesId = SeedIds.Movies.BlackSwan },
            new { DirectorsId = SeedIds.Directors.MartinScorsese, MoviesId = SeedIds.Movies.TheWolfOfWallStreet }
        );

        // Movie <-> Actor (ActorMovie join cedveli)
        modelBuilder.Entity("ActorMovie").HasData(
            new { ActorsId = SeedIds.Actors.LeonardoDiCaprio, MoviesId = SeedIds.Movies.Inception },
            new { ActorsId = SeedIds.Actors.TomHardy, MoviesId = SeedIds.Movies.Inception },
            new { ActorsId = SeedIds.Actors.CillianMurphy, MoviesId = SeedIds.Movies.Inception },
            new { ActorsId = SeedIds.Actors.ChristianBale, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { ActorsId = SeedIds.Actors.CillianMurphy, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { ActorsId = SeedIds.Actors.CillianMurphy, MoviesId = SeedIds.Movies.Oppenheimer },
            new { ActorsId = SeedIds.Actors.LeonardoDiCaprio, MoviesId = SeedIds.Movies.DjangoUnchained },
            new { ActorsId = SeedIds.Actors.MargotRobbie, MoviesId = SeedIds.Movies.Barbie },
            new { ActorsId = SeedIds.Actors.MerylStreep, MoviesId = SeedIds.Movies.LittleWomen },
            new { ActorsId = SeedIds.Actors.KeanuReeves, MoviesId = SeedIds.Movies.TheMatrix },
            new { ActorsId = SeedIds.Actors.TomHardy, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { ActorsId = SeedIds.Actors.CharlizeTheron, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { ActorsId = SeedIds.Actors.NataliePortman, MoviesId = SeedIds.Movies.BlackSwan },
            new { ActorsId = SeedIds.Actors.LeonardoDiCaprio, MoviesId = SeedIds.Movies.TheWolfOfWallStreet },
            new { ActorsId = SeedIds.Actors.MargotRobbie, MoviesId = SeedIds.Movies.TheWolfOfWallStreet }
        );

        // Movie <-> Genre (GenreMovie join cedveli)
        modelBuilder.Entity("GenreMovie").HasData(
            new { GenresId = SeedIds.Genres.Action, MoviesId = SeedIds.Movies.Inception },
            new { GenresId = SeedIds.Genres.ScienceFiction, MoviesId = SeedIds.Movies.Inception },
            new { GenresId = SeedIds.Genres.Thriller, MoviesId = SeedIds.Movies.Inception },
            new { GenresId = SeedIds.Genres.Action, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { GenresId = SeedIds.Genres.Crime, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.TheDarkKnight },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.Oppenheimer },
            new { GenresId = SeedIds.Genres.Historical, MoviesId = SeedIds.Movies.Oppenheimer },
            new { GenresId = SeedIds.Genres.Biography, MoviesId = SeedIds.Movies.Oppenheimer },
            new { GenresId = SeedIds.Genres.Adventure, MoviesId = SeedIds.Movies.Interstellar },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.Interstellar },
            new { GenresId = SeedIds.Genres.ScienceFiction, MoviesId = SeedIds.Movies.Interstellar },
            new { GenresId = SeedIds.Genres.Crime, MoviesId = SeedIds.Movies.PulpFiction },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.PulpFiction },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.DjangoUnchained },
            new { GenresId = SeedIds.Genres.Adventure, MoviesId = SeedIds.Movies.DjangoUnchained },
            new { GenresId = SeedIds.Genres.Crime, MoviesId = SeedIds.Movies.DjangoUnchained },
            new { GenresId = SeedIds.Genres.Comedy, MoviesId = SeedIds.Movies.Barbie },
            new { GenresId = SeedIds.Genres.Adventure, MoviesId = SeedIds.Movies.Barbie },
            new { GenresId = SeedIds.Genres.Fantasy, MoviesId = SeedIds.Movies.Barbie },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.LittleWomen },
            new { GenresId = SeedIds.Genres.Romance, MoviesId = SeedIds.Movies.LittleWomen },
            new { GenresId = SeedIds.Genres.Action, MoviesId = SeedIds.Movies.TheMatrix },
            new { GenresId = SeedIds.Genres.ScienceFiction, MoviesId = SeedIds.Movies.TheMatrix },
            new { GenresId = SeedIds.Genres.Action, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { GenresId = SeedIds.Genres.Adventure, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { GenresId = SeedIds.Genres.ScienceFiction, MoviesId = SeedIds.Movies.MadMaxFuryRoad },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.BlackSwan },
            new { GenresId = SeedIds.Genres.Thriller, MoviesId = SeedIds.Movies.BlackSwan },
            new { GenresId = SeedIds.Genres.Mystery, MoviesId = SeedIds.Movies.BlackSwan },
            new { GenresId = SeedIds.Genres.Crime, MoviesId = SeedIds.Movies.TheWolfOfWallStreet },
            new { GenresId = SeedIds.Genres.Comedy, MoviesId = SeedIds.Movies.TheWolfOfWallStreet },
            new { GenresId = SeedIds.Genres.Drama, MoviesId = SeedIds.Movies.TheWolfOfWallStreet },
            new { GenresId = SeedIds.Genres.Biography, MoviesId = SeedIds.Movies.TheWolfOfWallStreet }
        );
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                  
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
