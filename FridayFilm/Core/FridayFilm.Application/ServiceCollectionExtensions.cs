using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FridayFilm.Application // Əgər Extensions qovluğundadırsa, .Extensions əlavə et
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Bu kod Application qatındakı bütün Validator-ları avtomatik tapır və DI konteynerinə əlavə edir.
            // Hər dəfə yeni validator yazanda bura toxunmağa ehtiyac qalmayacaq.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}