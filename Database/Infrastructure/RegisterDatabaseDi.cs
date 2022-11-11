using Database.Models.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Infrastructure;

public static class RegisterDatabaseDi
{
    public static IServiceCollection Setup(this IServiceCollection service)
    {
        service.AddDbContext<ANSIContext>();

        return service;
    }
}