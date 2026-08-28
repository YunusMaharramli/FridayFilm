using FridayFilm.Application.Abstracts.Repositories;
using FridayFilm.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FridayFilm.Infrastructure;

public static class ServiceCollectionExtentions
{
    public static  IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
        return services;
    }
}
