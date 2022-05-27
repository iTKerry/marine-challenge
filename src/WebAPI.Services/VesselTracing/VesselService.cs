using CSharpFunctionalExtensions;
using DataAccess.EF;
using Domain.Entity;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services.VesselTracing;

public sealed class VesselService : IVesselService
{
    private readonly MarineContext _dbContext;

    public VesselService(MarineContext dbContext) => _dbContext = dbContext;

    public async Task<UnitResult<Exception>> TryUpdateVesselAsync(
        int imo, UpdateVesselDto data, CancellationToken ctx)
    {
        try
        {
            var domain = new Vessel
            {
                Id = new IMO(imo),
                Name = data.Name
            };
            
            var dbo = await _dbContext.Vessels.SingleAsync(v => v.Id == domain.Id, cancellationToken: ctx);
            dbo.Name = domain.Name;
            
            _dbContext.Update(dbo);

            return UnitResult.Success<Exception>();
        }
        catch (Exception exn)
        {
            return UnitResult.Failure(exn);
        }
    }
}