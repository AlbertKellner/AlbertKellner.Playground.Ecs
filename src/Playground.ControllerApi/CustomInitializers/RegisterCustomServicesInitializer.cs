using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Playground.Application.Infrastructure.Filter;
using Playground.Application.Infrastructure.Handlers;
using Playground.Application.Shared.Extensions;
using Playground.Configs;

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

                AddCacheProfile(options, durationInSeconds: 1, ResponseCacheProfile.For1Second);
                AddCacheProfile(options, durationInSeconds: 5, ResponseCacheProfile.For5Seconds);
                AddCacheProfile(options, durationInSeconds: 15, ResponseCacheProfile.For15Seconds);
                AddCacheProfile(options, durationInSeconds: 120, ResponseCacheProfile.For2Minutes);
            });

            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://pokeapi.co/api/v2"), name: "API Pokemon");

            return services;
        }

        private static void AddCacheProfile(MvcOptions options, int durationInSeconds, string profileName)
        {
            options.CacheProfiles.Add(profileName, new CacheProfile()
            {
                Duration = durationInSeconds,
                Location = ResponseCacheLocation.Any,
                VaryByHeader = "CorrelationId"
            });
        }

        public static void ConfigureMediatR(IServiceCollection services)
        {
            // Configure MediatR without assembly scanning to avoid reflection
            services.AddMediatR(cfg => { });

            services.AddRequestHandlers();
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
                options.OperationFilter<AddCustomHeaderOnOpenApiFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with 'Bearer ' into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
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
