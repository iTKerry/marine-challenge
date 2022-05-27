using Refit;
using WebAPP.Common;

namespace WebAPP.Data.API;

public interface IMarineAPI
{
    [Get("/api/vessels")]
    Task<Envelope<List<Vessel>>> GetVessels();

    [Get("/api/vessels/{imo}/traces")]
    Task<Envelope<List<Trace>>> GetTraces(int imo);

    [Put("/api/vessels/{imo}")]
    Task UpdateVessel(int imo, [Body] UpdateVesselRequest request);

    [Post("/api/traces")]
    Task AddTraces([Body] List<AddTraceRequest> traces);

    [Put("/api/traces/{traceId}")]
    Task UpdateTrace(int traceId, [Body] UpdateTraceRequest request);
}