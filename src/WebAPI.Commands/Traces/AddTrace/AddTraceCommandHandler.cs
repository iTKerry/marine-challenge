using Domain.Entity;
using Domain.ValueObjects;
using MediatR.Core.Abstractions;
using Microsoft.Extensions.Logging;
using WebAPI.Commands.Abstractions;
using WebAPI.Services.Abstractions;

namespace WebAPI.Commands.Traces.AddTrace;

public sealed class AddTraceCommandHandler : CommandHandlerBase<AddTraceCommand>
{
    private readonly ITracingService _tracingService;
    private readonly ILogger<AddTraceCommandHandler> _logger;

    public AddTraceCommandHandler(
        ITracingService tracingService, 
        ILogger<AddTraceCommandHandler> logger)
    {
        _tracingService = tracingService;
        _logger = logger;
    }

    public override async Task<IHandlerResult> Handle(AddTraceCommand request, CancellationToken ctx)
    {
        if (!request.Data.Any())
            return ValidationFailed("No data was provided");
        
        var vessels = request.Data
            .Select(dto => new Vessel
            {
                Id = new IMO(dto.IMO),
                Name = dto.VesselName
            })
            .DistinctBy(x => x.Id)
            .ToList();

        if (vessels.Count > 1)
            ValidationFailed("Received more than one vessel IMO");

        var traces = request.Data
            .Select(dto => new Trace
            {
                Point = new Coordinates(
                    new Latitude(dto.Point.Lat), 
                    new Longitude(dto.Point.Lon)),
                TimeStamp = dto.TimeStamp
            })
            .ToList();
        
        var vessel = vessels.First();
        var result = await _tracingService.TryAddTraceAsync(vessel, traces, ctx);

        if (result.IsFailure) 
            _logger.LogError(result.Error, "Error occurred while processing traces");

        return result.IsSuccess 
            ? Ok() 
            : ValidationFailed(result.Error.Message);
    }
}