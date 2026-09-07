using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Services;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Contexts;
using FridayFilm.Persistence.Repositories;
using FridayFilm.Persistence.Services;
using FridayFilm.Persistence.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Persistence;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        // Identity
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<FridayFilmDbContext>()
            .AddSignInManager();

        services.AddScoped<IAuthenticationService,AuthenticationService>();
        // Category
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        // Actor
        services.AddScoped<IActorReadRepository, ActorReadRepository>();
        services.AddScoped<IActorWriteRepository, ActorWriteRepository>();
        services.AddScoped<IActorService, ActorService>();

        // Director
        services.AddScoped<IDirectorReadRepository, DirectorReadRepository>();
        services.AddScoped<IDirectorWriteRepository, DirectorWriteRepository>();
        services.AddScoped<IDirectorService, DirectorService>();

        // Bio
        services.AddScoped<IBioReadRepository, BioReadRepository>();
        services.AddScoped<IBioWriteRepository, BioWriteRepository>();
        services.AddScoped<IBioService, BioService>();

        // Genre
        services.AddScoped<IGenreReadRepository, GenreReadRepository>();
        services.AddScoped<IGenreWriteRepository, GenreWriteRepository>();
        services.AddScoped<IGenreService, GenreService>();

        // MovieDetail 
        services.AddScoped<IMovieDetailReadRepository, MovieDetailReadRepository>();
        services.AddScoped<IMovieDetailWriteRepository, MovieDetailWriteRepository>();
        services.AddScoped<IMovieDetailService, MovieDetailService>();

        // Image & Gallery (Yeni əlavə edilənlər)
        services.AddScoped<IFilmImageReadRepository, FilmImageReadRepository>();
        services.AddScoped<IImageService, ImageService>();

        return services;
    }
}