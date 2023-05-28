using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Playground.Application.Shared;
using Playground.Application.Features.ToDoItems.Create.Interface;
using System.Data.SqlClient;
using Playground.Application.Features.ToDoItems.Create.Repositories;
using Playground.Application.Infrastructure.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomWebApplicationBuilderInitializer
    {
        public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            SerilogConfig();
            
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

            builder.Services.AddSingleton(settings);
        }

        private static void SerilogConfig(WebHostBuilderContext context)
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] [{CorrelationId}] [{ExecutionTime}] {Message:lj} {NewLine}{Exception}";

            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.With<ExecutionTimeEnricher>()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: outputTemplate);

            if (context.HostingEnvironment.IsDevelopment())
            {
                loggerConfiguration.WriteTo.File("log.txt", outputTemplate: outputTemplate);
            }

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.Register(container =>
            {
                //// Use configuration to get connection string
                //var configuration = container.Resolve<IConfiguration>();
                //var connectionString = configuration.GetConnectionString("TodoDatabase");

                //// Use a connection factory to create connections
                //var connectionFactory = container.Resolve<IDbConnectionFactory>();
                //var connection = connectionFactory.CreateConnection(connectionString);

                var connectionString = "Data Source=localhost;Initial Catalog=TodoDataBase;Integrated Security=SSPI";
                var connection = new SqlConnection(connectionString);

                return new CreateTodoItemRepository(connection);
            }).As<ICreateTodoItemRepository>();
        }
    }
}
