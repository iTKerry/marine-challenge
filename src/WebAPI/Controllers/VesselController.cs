using DataAccess.EF;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Commands.Vessels.UpdateVessel;
using WebAPI.Common;
using WebAPI.Controllers.Abstractions;
using WebAPI.Queries.GetTraces;
using WebAPI.Queries.GetVessels;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Controllers;

[Route("api/vessels")]
public class VesselController : ApiController
{
    public VesselController(IMediator mediator, MarineContext context) : base(mediator, context)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Envelope<List<VesselViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope<List<VesselViewModel>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetVesselsQuery(default, default, string.Empty, default);
        var result = await Mediator.Send(query);
        return FromQueryResult(result);
    }
    
    [HttpGet("{imo}/traces")]
    [ProducesResponseType(typeof(Envelope<List<VesselViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope<List<VesselViewModel>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVesselTraces(int imo)
    {
        var query = new GetTracesQuery(imo);
        var result = await Mediator.Send(query);
        return FromQueryResult(result);
    }
    
    [HttpPut("{imo}")]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateVessel(int imo, UpdateVesselDto data)
    {
        var command = new UpdateVesselCommand(imo, data);
        var result = await Mediator.Send(command);
        return await FromCommandResult(result);
    }
}