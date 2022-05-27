using WebAPP.Common;
using WebAPP.Data.Abstractions;
using WebAPP.Data.API;

namespace WebAPP.Data;

public sealed class TracesService : ITracesService
{
    private readonly IMarineAPI _marineApi;

    public TracesService(IMarineAPI marineApi)
    {
        _marineApi = marineApi;
    }

    public async Task<Envelope<List<Trace>>> GetTracesAsync(int imo)
    {
        var result = await _marineApi.GetTraces(imo);
        return result;
    }

    public async Task UpdateTraceAsync(Trace trace)
    {
        var request = new UpdateTraceRequest
        {
            TimeStamp = trace.TimeStamp,
            Point = new TracePoint
            {
                Lat = trace.Latitude,
                Lon = trace.Longitude
            }
        };

        await _marineApi.UpdateTrace(trace.TraceId, request);
    }
}
