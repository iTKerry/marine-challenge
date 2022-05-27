using MediatR.Core;
using MediatR.Core.Abstractions;

namespace WebAPI.Commands.Abstractions;

public abstract class CommandHandlerBase<TRequest> : RequestHandlerBase<TRequest> 
    where TRequest : ICommand
{
}