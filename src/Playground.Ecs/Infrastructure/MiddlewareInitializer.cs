using Swashbuckle.AspNetCore.SwaggerUI;

namespace Playground.MinimalApi.Infrastructure
{
    public static partial class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            return app;
        }

        //public static WebApplication UseSwaggerUI(this WebApplication app)
        //{
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v0.1");
        //        c.RoutePrefix = string.Empty;
        //    });

        //    return app;
        //}
    }
}
