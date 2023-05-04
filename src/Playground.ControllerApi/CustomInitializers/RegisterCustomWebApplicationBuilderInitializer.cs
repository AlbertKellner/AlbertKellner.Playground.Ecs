using Autofac;
using Playground.Application.Shared.AutofacModules;
using Autofac.Extensions.DependencyInjection;
using Serilog;

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

            return builder;
        }

        private static void SerilogConfig()
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}";

            Log.Logger =
                    new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Information()
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File("log.txt", outputTemplate: outputTemplate)
                        .CreateLogger();
        }
    }
}
