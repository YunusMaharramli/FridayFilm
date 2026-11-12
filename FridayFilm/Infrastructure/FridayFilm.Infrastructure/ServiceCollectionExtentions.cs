using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Infrastructure;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddKeyedScoped<IFileService, LocalFileService>("local");
        services.AddKeyedScoped<IFileService, CloudinaryFileService>("cloudinary");

        return services;
    }
}
