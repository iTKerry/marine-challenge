using CSharpFunctionalExtensions;
using Domain.Entity;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Services.Abstractions;

public interface ITracingService
{
    Task<UnitResult<Exception>> TryUpdateTraceAsync(int traceId, UpdateTraceDto trace, CancellationToken ctx);
    Task<UnitResult<Exception>> TryAddTraceAsync(Vessel vessel, List<Trace> traces, CancellationToken ctx);
}