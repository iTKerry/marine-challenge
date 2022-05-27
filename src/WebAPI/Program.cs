using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataAccess.EF;
using MediatR;
using Serilog;
using WebAPI.Services;

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
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddOptions();
    
    services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
    
    services.Configure<MarineDbConnectionString>(cs => cs.Value = cfg.GetConnectionString(MarineDbConnectionString.Key));
    
    services.AddDataAccessModule();
    services.AddServicesModule();
}

void ConfigureApplication(IApplicationBuilder app, IHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }
    else
    {
        app.UseHsts();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseRouting();
}

void ConfigureRoutes(IEndpointRouteBuilder router)
{
    router.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
}

// configure builder
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory(cfg => cfg.RegisterAssemblyModules(typeof(Program).Assembly)));
ConfigureLogging(builder.Logging);
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

// configure application
var app = builder.Build();
ConfigureApplication(app, builder.Environment);
ConfigureRoutes(app);

// run
await app.MigrateDatabaseAsync();
await app.RunAsync();