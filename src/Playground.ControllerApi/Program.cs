using Playground.Application.Infrastructure.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.RegisterCustomWebApplicationBuilder();

builder.Services.RegisterCustomServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// Add configuration providers
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Bind the configuration to the settings class
var settings = new ExternalApiOptions();
builder.Configuration.GetSection("ExternalApiOptions").Bind(settings);

// Use the settings in your application
builder.Services.AddSingleton(settings);


var app = builder.Build();

app.RegisterCustomMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

FlushLogsBeforeCloseApplication();

/// <summary>
/// Para garantir que os logs sejam descartados corretamente ao encerrar o aplicativo
/// </summary>
static void FlushLogsBeforeCloseApplication()
{
    Log.CloseAndFlush();
}