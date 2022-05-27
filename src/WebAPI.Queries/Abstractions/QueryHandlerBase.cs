using MediatR.Core;
using MediatR.Core.Abstractions;

namespace WebAPI.Queries.Abstractions;

public abstract class QueryHandlerBase<TRequest, TResponse> 
    : RequestHandlerBase<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
}