using FridayFilm.Domain.Common;
using FridayFilm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FridayFilm.Persistence.Contexts;

public class FridayFilmDbContext:DbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
 
        var seedDate = new DateTime(2026, 8, 31, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Aksiya", Slug = "aksiya", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Komediya", Slug = "komediya", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Dram", Slug = "dram", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Qorxu", Slug = "qorxu", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Elmi Fantastika", Slug = "elmi-fantastika", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Romantika", Slug = "romantika", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Triller", Slug = "triller", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Sənədli", Slug = "senedli", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Fantastika", Slug = "fantastika", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Animasiya", Slug = "animasiya", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Müəmma", Slug = "muemma", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Macəra", Slug = "macera", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Cinayət", Slug = "cinayet", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Ailə", Slug = "aile", CreatedDate = seedDate },
            new Category { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Tarixi", Slug = "tarixi", CreatedDate = seedDate }
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
