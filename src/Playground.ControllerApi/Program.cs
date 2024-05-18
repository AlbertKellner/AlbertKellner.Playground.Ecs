using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Text.Json;

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
            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    exception = entry.Value.Exception?.Message ?? "none",
                    duration = entry.Value.Duration.ToString()
                })
            };
            await JsonSerializer.SerializeAsync(context.Response.Body, response, new JsonSerializerOptions { WriteIndented = true });
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
