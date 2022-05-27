using CSharpFunctionalExtensions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Services.Abstractions;

public interface IVesselService
{
    Task<UnitResult<Exception>> TryUpdateVesselAsync(int imo, UpdateVesselDto data, CancellationToken ctx);
}