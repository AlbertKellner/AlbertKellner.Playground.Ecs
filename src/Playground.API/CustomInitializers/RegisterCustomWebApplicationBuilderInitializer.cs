using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Playground.Application.Shared;
using Playground.Application.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Playground.Application.Shared.Domain;
using MySqlConnector;
using Serilog.Events;
using Autofac.Core;
using Playground.API.Controllers;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomWebApplicationBuilderInitializer
    {
        public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            SerilogConfig(builder, builder.Environment);

            ServiceProviderFactory(builder);

            LoadEnvironmentOptions(builder);

            return builder;
        }

        private static void ServiceProviderFactory(WebApplicationBuilder builder) =>
            builder.Host.UseServiceProviderFactory<ContainerBuilder>(new AutofacServiceProviderFactory())
                .ConfigureContainer((Action<ContainerBuilder>)(builder =>
                {
                    builder.RegisterModule(new HandlersModule());
                    builder.RegisterModule(new ApiModule());
                    RegisterDependencies(builder);
                }));

        private static void LoadEnvironmentOptions(WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            var settings = new ExternalApiOptions();
            builder.Configuration.GetSection("ExternalApiOptions").Bind(settings);
            builder.Configuration.GetSection("ConnectionStrings").Bind(settings);

            builder.Services.AddSingleton(settings);
        }

        private static void SerilogConfig(WebApplicationBuilder builder, IWebHostEnvironment environment)
        {
            const string outputTemplateWithoutProperties = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] [{CorrelationId}] [{ExecutionTime}] [{ExecutionTimeSinceLastLog}] {Message:lj} {NewLine}{Exception}";

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration) // Reads settings from appsettings.json
                .Enrich.FromLogContext()
                .Enrich.With<LogEnricher>()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.WriteTo.Console(outputTemplate: outputTemplateWithoutProperties);
                .WriteTo.Async(a => a.Console(outputTemplate: outputTemplateWithoutProperties));

            //if (environment.IsDevelopment())
            //{
            //    loggerConfiguration.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, outputTemplate: outputTemplateWithoutProperties);
            //}

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            ThreadPool.SetMinThreads(50, 50);

        }
    }
}
