using Playground.Middleware;

namespace Microsoft.AspNetCore.Builder
{
    public static partial class RegisterCustomMiddlewareInitializer
    {
        public static WebApplication RegisterCustomMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.ConfigureSwagger();
            }

            app.UseMiddleware<CorrelationIdMiddleware>();

            return app;
        }

        public static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Api v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Test Api v2");
            });
        }
    }
}
