using DataAccess.EF;
using MediatR.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Queries.Abstractions;

namespace WebAPI.Queries.GetVessels;

public sealed class GetVesselsQueryHandler : QueryHandlerBase<GetVesselsQuery, List<VesselViewModel>>
{
    private readonly MarineContext _dbContext;

    public GetVesselsQueryHandler(MarineContext dbContext) => _dbContext = dbContext;

    public override async Task<IHandlerResult<List<VesselViewModel>>> Handle(
        GetVesselsQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Vessels
            .Select(dbo => new VesselViewModel
            {
                IMO = dbo.Id.Value,
                Name = dbo.Name,
                LastTimeStamp = dbo.Traces
                    .OrderByDescending(t => t.TimeStamp)
                    .Select(t => t.TimeStamp)
                    .First()
            })
            .OrderByDescending(x => x.LastTimeStamp)
            .ToListAsync(cancellationToken: cancellationToken);

        return Data(data);
    }
}