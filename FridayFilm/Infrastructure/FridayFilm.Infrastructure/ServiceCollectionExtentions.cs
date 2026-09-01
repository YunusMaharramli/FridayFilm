using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Services;
using FridayFilm.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FridayFilm.Infrastructure;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IFileService, LocalFileService>();
       
        return services;
    }
}
