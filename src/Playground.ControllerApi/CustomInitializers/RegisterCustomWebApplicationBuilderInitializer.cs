using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;
using Playground.Application.Shared;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomWebApplicationBuilderInitializer
    {
        public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(this WebApplicationBuilder builder)
        {
            SerilogConfig();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                        .ConfigureContainer<ContainerBuilder>(builder =>
                        {
                            builder.RegisterModule(new HandlersModule());
                        });

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
    }
}
