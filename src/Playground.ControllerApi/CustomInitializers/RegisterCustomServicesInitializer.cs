using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Playground.Application.Infrastructure.Filter;
using Playground.Application.Infrastructure.Handlers;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomServicesInitializer
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            ConfigureMediatR(services);

            RegisterCustomDependencies(services);

            RegisterSwagger(services);

            RegisterAddApiVersioning(services);

            RegisterAddVersionedApiExplorer(services);

            services.AddMvc().AddControllersAsServices();

            services.AddTransient<CorrelationIdHandler>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(LogActionFilter));
                options.Filters.Add<HttpGlobalExceptionFilter>();

                options.CacheProfiles.Add("ResponseCache:1Second", new CacheProfile()
                {
                    Duration = 1,
                    Location = ResponseCacheLocation.Any,
                    VaryByHeader = "Token",
                    VaryByQueryKeys = new[] { "*" }
                });

                options.CacheProfiles.Add("ResponseCache:5Seconds", new CacheProfile()
                {
                    Duration = 5,
                    Location = ResponseCacheLocation.Any
                });

                options.CacheProfiles.Add("ResponseCache:30Seconds", new CacheProfile()
                {
                    Duration = 30,
                    Location = ResponseCacheLocation.Any
                });

                options.CacheProfiles.Add("ResponseCache:2Minutes", new CacheProfile()
                {
                    Duration = 120,
                    Location = ResponseCacheLocation.Any
                });
            });

            return services;
        }

        public static void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(cfg =>
             {
                 cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
             });
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
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Playground.API - version 1.0",
                        Description = "Playground"
                    });

                options.SwaggerDoc("v2",
                    new OpenApiInfo
                    {
                        Version = "v2",
                        Title = "Playground.API - version 2.0",
                        Description = "Playground"
                    });

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.OperationFilter<AddCorrelationIdHeaderOnOpenApiFilter>();
            });
        }

        private static void RegisterAddApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Mvc.ApiVersion(1, 0);
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
