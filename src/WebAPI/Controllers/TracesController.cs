using DataAccess.EF;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Commands.Traces.AddTrace;
using WebAPI.Commands.Traces.UpdateTrace;
using WebAPI.Common;
using WebAPI.Controllers.Abstractions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Controllers;

[Route("api/traces")]
public class TracesController : ApiController
{
    public TracesController(IMediator mediator, MarineContext context) : base(mediator, context)
    {
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Traces(AddTraceDto[] data)
    {
        var command = new AddTraceCommand(data);
        var result = await Mediator.Send(command);
        return await FromCommandResult(result);
    }
    
    [HttpPut("{traceId}")]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTrace(int traceId, UpdateTraceDto data)
    {
        var command = new UpdateTraceCommand(traceId, data);
        var result = await Mediator.Send(command);
        return await FromCommandResult(result);
    }

}