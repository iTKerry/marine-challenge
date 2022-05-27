using DataAccess.EF.Models;
using Domain.ValueObjects;

namespace DataAccess.EF.Seeder;

public sealed class DbSeeder : IDbSeeder
{
    private readonly MarineContext _dbContext;

    public DbSeeder(MarineContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedDataAsync()
    {
        await _dbContext.AddRangeAsync(Vessels);
        await _dbContext.SaveChangesAsync();
    }

    private static readonly List<VesselDbo> Vessels = new()
    {
        new VesselDbo
        {
            Id = new IMO(1),
            Name = "First Vessel",
            Traces = new List<TraceDbo>
            {
                new()
                {
                    Latitude = 0.1,
                    Longitude = 0.1,
                    TimeStamp = DateTime.Now.AddHours(-1)
                },
                new()
                {
                    Latitude = 0.2,
                    Longitude = 0.2,
                    TimeStamp = DateTime.Now.AddHours(-2)
                },
            }
        },
        
        new VesselDbo
        {
            Id = new IMO(2),
            Name = "Second Vessel",
            Traces = new List<TraceDbo>
            {
                new()
                {
                    Latitude = 0.3,
                    Longitude = 0.3,
                    TimeStamp = DateTime.Now.AddHours(-3)
                },
                new()
                {
                    Latitude = 0.4,
                    Longitude = 0.4,
                    TimeStamp = DateTime.Now.AddHours(-4)
                },
            }
        },
    };
}