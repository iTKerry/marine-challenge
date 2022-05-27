using WebAPP.Common;
using WebAPP.Data.Abstractions;
using WebAPP.Data.API;

namespace WebAPP.Data;

public sealed class VesselsService : IVesselsService
{
    private readonly IMarineAPI _marineApi;

    public VesselsService(IMarineAPI marineApi)
    {
        _marineApi = marineApi;
    }

    public async Task<Envelope<List<Vessel>>> GetVesselsAsync()
    {
        var result = await _marineApi.GetVessels();
        return result;
    }

    public async Task UpdateVesselAsync(Vessel vessel)
    {
        var request = new UpdateVesselRequest
        {
            Name = vessel.Name
        };

        await _marineApi.UpdateVessel(vessel.IMO, request);
    }
}
