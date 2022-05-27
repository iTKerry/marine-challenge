using DataAccess.EF.Seeder;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.EF;

public static class Module
{
    public static IServiceCollection AddDataAccessModule(this IServiceCollection services)
    {
        services.AddDbContext<MarineContext>();
        services.AddTransient<IDbSeeder, DbSeeder>();
        
        return services;
    }
}