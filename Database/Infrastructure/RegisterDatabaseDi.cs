using Database.Repository;
using Database.Services;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Database.Infrastructure;

public static class RegisterDatabaseDi
{
    public static IServiceCollection SetupDatabaseDi(this IServiceCollection service, string databaseConnectionString)
    {
        service
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        service
            .AddSingleton(new MySqlConnection(databaseConnectionString))
            .AddDbContext<ANSIContext>();

        service
            .AddScoped<IDishQueryExecutorService, DishQueryExecutorService>()
            .AddScoped<IDishRepository, DishRepository>();
        return service;
    }
}