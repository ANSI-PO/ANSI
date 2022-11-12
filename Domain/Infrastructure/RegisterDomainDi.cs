using Database.Infrastructure;
using Domain.Abstractions;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Infrastructure;

public static class DependencyInjectionContainerRegistration
{
    public static IServiceCollection SetupDomainDi(this IServiceCollection service)
    {
        service
            .SetupDatabaseDi("server=localhost;port=3306;database=ANSI;uid=root;pwd=DevUserPassword");

        service
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        service
            .AddScoped<IDishDatabase, DishDatabase>()
            .AddScoped<IDishSelectorService, DishSelectorService>()
            .AddSingleton<IDishQueryBuilder, DishQueryBuilder>();

        return service;
    }
}