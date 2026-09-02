using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Application.Abstracts.Services;
using FridayFilm.Application.Services;
using FridayFilm.Infrastructure.Repositories;
using FridayFilm.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Persistence;

public static class ServiceCollectionExtentions
{
    public static  IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
       services.AddScoped<IActorReadRepository, ActorReadRepository>();
        services.AddScoped<IActorWriteRepository, ActorWriteRepository>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IDirectorService, DirectorService>();
        services.AddScoped<IDirectorReadRepository, DirectorReadRepository>();
        services.AddScoped<IDirectorWriteRepository, DirectorWriteRepository>();
        services.AddScoped<IBioReadRepository, BioReadRepository>();
        services.AddScoped<IBioWriteRepository, BioWriteRepository>();
        services.AddScoped<IBioService, BioService>();
        return services;
    }
}
