using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Playground.Application.Shared.AutofacModules;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
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

        app.Run();
    }
}