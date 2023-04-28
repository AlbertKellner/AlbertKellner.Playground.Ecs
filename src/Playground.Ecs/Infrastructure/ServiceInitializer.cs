namespace Playground.MinimalApi.Infrastructure
{
    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(
                                            this IServiceCollection services)
        {
            RegisterCustomDependencies(services);

            RegisterSwagger(services);
            return services;
        }

        private static void RegisterCustomDependencies(IServiceCollection services)
        {
            //services.AddTransient<IInitialRespositoryAPI, InitialRespositoryAPI>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}