using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Text.Json;
using Playground;
using Playground.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.RegisterCustomWebApplicationBuilder();
builder.Services.RegisterCustomServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.RegisterCustomMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var response = new HealthCheckResponse(
                report.Status.ToString(),
                report.Entries.Select(entry =>
                    new HealthCheckEntry(
                        entry.Key,
                        entry.Value.Status.ToString(),
                        entry.Value.Exception?.Message ?? "none",
                        entry.Value.Duration.ToString())));

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, ApiJsonContext.Default.HealthCheckResponse));
        }
    });
});

app.Run();

FlushLogsBeforeCloseApplication();

/// <summary>
/// Para garantir que os logs sejam descartados corretamente ao encerrar o aplicativo
/// </summary>
static void FlushLogsBeforeCloseApplication()
{
    Log.CloseAndFlush();
}
