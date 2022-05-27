using Microsoft.Extensions.Options;
using Refit;
using Serilog;
using WebAPP;
using WebAPP.Data;
using WebAPP.Data.Abstractions;
using WebAPP.Data.API;

void ConfigureLogging(ILoggingBuilder loggingBuilder)
{
    var logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .CreateLogger();
    
    Log.Logger = logger;

    loggingBuilder
        .ClearProviders()
        .AddSerilog(logger);
}

void ConfigureServices(IServiceCollection services, IConfiguration cfg, IHostEnvironment env)
{
    services.AddOptions();
    services.Configure<MarineApiOptions>(options => cfg.GetSection("MarineApi").Bind(options));
    
    services.AddRefitClient<IMarineAPI>()
        .ConfigureHttpClient((provider, client) =>
        {
            var option = provider.GetRequiredService<IOptions<MarineApiOptions>>(); 
            client.BaseAddress = new Uri(option.Value.Url);
        });
    
    services.AddRazorPages();
    services.AddServerSideBlazor();
    
    services.AddTransient<IVesselsService, VesselsService>();
    services.AddTransient<ITracesService, TracesService>();
}

void ConfigureApplication(IApplicationBuilder app, IHostEnvironment env)
{
    if (!env.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
}

void ConfigureRoutes(IEndpointRouteBuilder router)
{
    router.MapBlazorHub();
    router.MapFallbackToPage("/_Host");
}

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging(builder.Logging);
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();
ConfigureApplication(app, builder.Environment);
ConfigureRoutes(app);

await app.RunAsync();

namespace WebAPP
{
    public record MarineApiOptions
    {
        public string Url { get; set; }
    };
}
