using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Playground.Filter;
using Playground.Handlers;

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
            });

            //services.AddRefitClient<IMyApiClient>()
            //    .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["ApiBaseUrl"]))
            //    .AddHttpMessageHandler<CorrelationIdHandler>();

            //services.AddHttpClient("MyHttpClient")
            //    .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["ApiBaseUrl"]))
            //    .AddHttpMessageHandler<CorrelationIdHandler>();

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
                options.OperationFilter<AddCorrelationIdHeaderFilter>();
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
