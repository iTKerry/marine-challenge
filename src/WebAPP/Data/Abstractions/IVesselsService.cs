using WebAPP.Common;
using WebAPP.Data.API;

namespace WebAPP.Data.Abstractions;

public interface IVesselsService
{
    Task<Envelope<List<Vessel>>> GetVesselsAsync();
    Task UpdateVesselAsync(Vessel vessel);
}