using MediatR.Core.Abstractions;
using Microsoft.Extensions.Logging;
using WebAPI.Commands.Abstractions;
using WebAPI.Services.Abstractions;

namespace WebAPI.Commands.Traces.UpdateTrace;

public sealed class UpdateTraceCommandHandler : CommandHandlerBase<UpdateTraceCommand>
{
    private readonly ITracingService _tracingService;
    private readonly ILogger<UpdateTraceCommandHandler> _logger;

    public UpdateTraceCommandHandler(
        ITracingService tracingService, 
        ILogger<UpdateTraceCommandHandler> logger)
    {
        _tracingService = tracingService;
        _logger = logger;
    }

    public override async Task<IHandlerResult> Handle(UpdateTraceCommand request, CancellationToken ctx)
    {
        var result = await _tracingService.TryUpdateTraceAsync(request.TraceId, request.Data, ctx);
        
        if (result.IsFailure) 
            _logger.LogError(result.Error, "Error occurred while updating trace: {Request}", request);

        return result.IsSuccess 
            ? Ok() 
            : ValidationFailed(result.Error.Message);
    }
}