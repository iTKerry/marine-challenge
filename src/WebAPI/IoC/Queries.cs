using System.Reflection;
using Autofac;
using MediatR;
using WebAPI.Queries.Abstractions;
using Module = Autofac.Module;

namespace WebAPI.IoC;

public class Queries : Module
{
    protected override Assembly ThisAssembly => typeof(QueryHandlerBase<,>).Assembly;

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>))
            .AsImplementedInterfaces();
    }
}