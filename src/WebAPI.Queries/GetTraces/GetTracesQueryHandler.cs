using DataAccess.EF;
using Domain.ValueObjects;
using MediatR.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Queries.Abstractions;

namespace WebAPI.Queries.GetTraces;

public sealed class GetTracesQueryHandler : QueryHandlerBase<GetTracesQuery, List<TraceViewModel>>
{
    private readonly MarineContext _dbContext;
    private readonly ILogger<GetTracesQueryHandler> _logger;

    public GetTracesQueryHandler(MarineContext dbContext, ILogger<GetTracesQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task<IHandlerResult<List<TraceViewModel>>> Handle(
        GetTracesQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _dbContext.Traces
            .Include(t => t.Vessel)
            .Where(t => t.Vessel.Id == new IMO(request.IMO))
            .Select(t => new TraceViewModel
            {
                TraceId = t.Id,
                TimeStamp = t.TimeStamp,
                Latitude = t.Latitude,
                Longitude = t.Longitude
            })
            .ToListAsync(cancellationToken);

        return Data(result);
    }
}