using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Services;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Repositories;
using FridayFilm.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Persistence;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
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