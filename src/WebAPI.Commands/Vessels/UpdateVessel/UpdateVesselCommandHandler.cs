using MediatR.Core.Abstractions;
using Microsoft.Extensions.Logging;
using WebAPI.Commands.Abstractions;
using WebAPI.Services.Abstractions;

namespace WebAPI.Commands.Vessels.UpdateVessel;

public sealed class UpdateVesselCommandHandler : CommandHandlerBase<UpdateVesselCommand>
{
    private readonly IVesselService _vesselService;
    private readonly ILogger<UpdateVesselCommandHandler> _logger;

    public UpdateVesselCommandHandler(
        IVesselService vesselService, 
        ILogger<UpdateVesselCommandHandler> logger)
    {
        _vesselService = vesselService;
        _logger = logger;
    }

    public override async Task<IHandlerResult> Handle(UpdateVesselCommand request, CancellationToken ctx)
    {
        var result = await _vesselService.TryUpdateVesselAsync(request.IMO, request.Data, ctx);
        
        if (result.IsFailure) 
            _logger.LogError(result.Error, "Error occurred while updating vessel: {Request}", request);

        return result.IsSuccess 
            ? Ok() 
            : ValidationFailed(result.Error.Message);
    }
}