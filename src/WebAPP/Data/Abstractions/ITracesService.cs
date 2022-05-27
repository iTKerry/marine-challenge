using WebAPP.Common;
using WebAPP.Data.API;

namespace WebAPP.Data.Abstractions;

public interface ITracesService
{
    Task<Envelope<List<Trace>>> GetTracesAsync(int imo);
    Task UpdateTraceAsync(Trace trace);
}