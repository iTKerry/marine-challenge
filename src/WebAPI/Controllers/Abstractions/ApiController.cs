using System.Net.Mime;
using DataAccess.EF;
using MediatR;
using MediatR.Core;
using MediatR.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;

namespace WebAPI.Controllers.Abstractions;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class ApiController : ControllerBase
{
    protected readonly MarineContext DbContext;
    protected IMediator Mediator { get; }

    protected ApiController(IMediator mediator, MarineContext dbContext) => 
        (Mediator, DbContext) = (mediator, dbContext);

    protected IActionResult FromQueryResult<T>(IHandlerResult<T> result) where T : class =>
        result switch
        {
            DataHandlerResult<T> res =>
                Ok(Envelope.Ok(res.Data)),

            PagedDataHandlerResult<T> res =>
                Ok(Envelope.Ok(res)),

            ValidationFailedHandlerResult<T> res =>
                BadRequest(Envelope.Error(res.Message)),

            _ => NotFound()
        };

    protected async Task<IActionResult> FromCommandResult(IHandlerResult result)
    {
        if (result is OkHandlerResult)
        {
            await DbContext.SaveChangesAsync();
        }
        
        return result switch
        {
            OkHandlerResult => Ok(Envelope.Ok()),

            ValidationFailedHandlerResult res => BadRequest(Envelope.Error(res.Message)),
            _ => NotFound()
        };
    }
}