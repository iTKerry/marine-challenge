using CSharpFunctionalExtensions;
using DataAccess.EF;
using DataAccess.EF.Models;
using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services.VesselTracing;

public sealed class TracingService : ITracingService
{
    private readonly MarineContext _dbContext;

    public TracingService(MarineContext dbContext) => _dbContext = dbContext;

    public async Task<UnitResult<Exception>> TryAddTraceAsync(Vessel vessel, List<Trace> traces, CancellationToken ctx)
    {
        try
        {
            var dboVessel = await _dbContext.Vessels
                .AsTracking()
                .SingleOrDefaultAsync(v => v.Id == vessel.Id, ctx);
            if (dboVessel is null)
            {
                dboVessel = new VesselDbo
                {
                    Id = vessel.Id,
                    Name = vessel.Name,
                    Traces = new List<TraceDbo>()
                };
                
                await _dbContext.AddAsync(dboVessel, ctx);
            }
            else
            {
                dboVessel.Name = vessel.Name;
                _dbContext.Update(dboVessel);
            }

            var dboTraces = traces
                .Select(t => new TraceDbo
                {
                    TimeStamp = t.TimeStamp,
                    Latitude = t.Point.Latitude.Value,
                    Longitude = t.Point.Longitude.Value,
                    Vessel = dboVessel
                })
                .ToList();
            
            _dbContext.AddRange(dboTraces);
                
            return UnitResult.Success<Exception>();
        }
        catch (Exception enx)
        {
            return UnitResult.Failure(enx);
        }
    }

    public async Task<UnitResult<Exception>> TryUpdateTraceAsync(int traceId, UpdateTraceDto trace, CancellationToken ctx)
    {
        try
        {
            var domainData = new Trace
            {
                Id = traceId,
                TimeStamp = trace.TimeStamp,
                Point = new Coordinates(
                    new Latitude(trace.Point.Lat),
                    new Longitude(trace.Point.Lon))
            };

            var dboData = await _dbContext.Traces
                .AsTracking()
                .SingleAsync(vt => vt.Id == domainData.Id, cancellationToken: ctx);

            dboData.TimeStamp = domainData.TimeStamp;
            dboData.Latitude = domainData.Point.Latitude.Value;
            dboData.Longitude = domainData.Point.Longitude.Value;
            
            return UnitResult.Success<Exception>();
        }
        catch (Exception enx)
        {
            return UnitResult.Failure(enx);
        }
    }
}