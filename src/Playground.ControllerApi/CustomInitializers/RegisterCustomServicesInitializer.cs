using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomServicesInitializer
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            RegisterCustomDependencies(services);

            RegisterSwagger(services);

            RegisterAddApiVersioning(services);

            RegisterAddVersionedApiExplorer(services);

            return services;
        }

        private static void RegisterCustomDependencies(IServiceCollection services)
        {
            //services.AddTransient<IInitialRespositoryAPI, InitialRespositoryAPI>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                // For each supported version, add a new SwaggerDoc
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Test Api v1",
                        Description = "A test API version 1.0"
                    });

                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Version = "v2",
                        Title = "Test Api v2",
                        Description = "A test API version 2.0"
                    });

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        private static void RegisterAddApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });
        }

        private static void RegisterAddVersionedApiExplorer(IServiceCollection services)
        {
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
