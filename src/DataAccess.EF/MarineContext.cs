using DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAccess.EF;

public class MarineContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly MarineDbConnectionString _connectionString;
    
    public DbSet<VesselDbo> Vessels { get; set; }
    public DbSet<TraceDbo> Traces { get; set; }

    public MarineContext(ILoggerFactory loggerFactory, IOptions<MarineDbConnectionString> connectionStringOption)
    {
        _loggerFactory = loggerFactory;
        _connectionString = connectionStringOption.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .UseLoggerFactory(_loggerFactory)
            .UseSqlServer(_connectionString.Value);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(MarineContext).Assembly);
    }
}