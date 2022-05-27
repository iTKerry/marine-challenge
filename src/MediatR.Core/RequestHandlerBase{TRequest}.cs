using MediatR.Core.Abstractions;

namespace MediatR.Core;

public abstract class RequestHandlerBase<TRequest> : IRequestHandler<TRequest, IHandlerResult>
    where TRequest : IRequest<IHandlerResult>
{
    public abstract Task<IHandlerResult> Handle(TRequest request, CancellationToken ctx);

    protected static IHandlerResult Ok() =>
        new OkHandlerResult();

    protected static IHandlerResult NotFound() =>
        new NotFoundHandlerResult();

    protected static IHandlerResult ValidationFailed(string message) =>
        new ValidationFailedHandlerResult(message);
}