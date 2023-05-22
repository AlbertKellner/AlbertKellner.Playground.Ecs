using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Playground.Application.Shared;
using Playground.Application.Features.ToDoItems.Create.Interface;
using System.Data.SqlClient;
using Playground.Application.Features.ToDoItems.Create.Repositories;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomWebApplicationBuilderInitializer
    {
        public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            SerilogConfig();

            builder.Host.UseServiceProviderFactory<ContainerBuilder>(new AutofacServiceProviderFactory())
                        .ConfigureContainer((Action<ContainerBuilder>)(builder =>
                        {
                            builder.RegisterModule(new HandlersModule());
                            builder.RegisterModule(new ApiModule());
                            RegisterDependencies(builder);
                        }));

            return builder;
        }

        private static void SerilogConfig()
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss.fff} {Level:u3}] [{CorrelationId}] [{ExecutionTime}] {Message:lj} {NewLine}{Exception}";

            Log.Logger =
                    new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.With<ExecutionTimeEnricher>()
                        .MinimumLevel.Information()
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File("log.txt", outputTemplate: outputTemplate)
                        .CreateLogger();
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
