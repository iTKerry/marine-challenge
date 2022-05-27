using System.Reflection;
using Autofac;
using MediatR;
using WebAPI.Commands.Abstractions;
using Module = Autofac.Module;

namespace WebAPI.IoC;

public class Commands : Module
{
    protected override Assembly ThisAssembly => typeof(CommandHandlerBase<>).Assembly;

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))
            .AsImplementedInterfaces();
    }
}