using DataAccess.EF.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataAccess.EF;

public static class MigrationManager
{
    public static async Task<IHost> MigrateDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        await using var ctx = scope.ServiceProvider.GetRequiredService<MarineContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

        await ctx.Database.EnsureDeletedAsync();
        await ctx.Database.MigrateAsync();
        await seeder.SeedDataAsync();

        return host;
    }
}