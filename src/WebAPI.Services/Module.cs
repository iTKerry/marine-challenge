using Microsoft.Extensions.DependencyInjection;
using WebAPI.Services.Abstractions;
using WebAPI.Services.VesselTracing;

namespace WebAPI.Services;

public static class ServicesModule
{
    public static IServiceCollection AddServicesModule(this IServiceCollection services)
    {
        services.AddTransient<ITracingService, TracingService>();
        services.AddTransient<IVesselService, VesselService>();
        
        return services;
    }
}