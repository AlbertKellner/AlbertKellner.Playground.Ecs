﻿using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Playground.Application.Shared;
using Playground.Application.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Playground.Application.Features.Country.Command.Create.Repositories;
using Playground.Application.Features.Country.Command.Create.Interface;
using Playground.Application.Shared.Domain;
using MySqlConnector;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomWebApplicationBuilderInitializer
    {
        public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            SerilogConfig(builder.Environment);
            
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

        private static void SerilogConfig(IWebHostEnvironment environment)
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] [{CorrelationId}] [{ExecutionTime}] {Message:lj} {NewLine}{Exception}";

            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With<ExecutionTimeEnricher>()
                .MinimumLevel.Information()
                .WriteTo.Async(a => a.Console(outputTemplate: outputTemplate));

            if (environment.IsDevelopment())
            {
                loggerConfiguration.WriteTo.File("log.txt", outputTemplate: outputTemplate);
            }

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            ThreadPool.SetMinThreads(50, 50);

            builder.Register(container =>
            {
                var memoryCache = container.Resolve<IMemoryCache>();
                var databaseOptions = container.Resolve<IOptions<ConnectionstringsOptions>>();

                //var connection = new MySqlConnection(databaseOptions.Value.MySqlConnectionString);
                var connection = new MySqlConnection("server=localhost;user id=admin;password=123456;database=world;");

                return new GetAllCountryRepository(connection, memoryCache);
            }).As<IGetAllCountryRepository>();
        }
    }
}
