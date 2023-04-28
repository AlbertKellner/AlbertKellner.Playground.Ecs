using Swashbuckle.AspNetCore.SwaggerUI;

namespace Playground.MinimalApi.EndPoints
{
    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapInitialEndPoints();
            app.MapRandomEndPoints();
            app.MinimalToDoListEndpoints();

            return app;
        }

        public static WebApplication MapInitialEndPoints(this WebApplication app)
        {
            //app.MapGet("/initial/{topic?}", (string? topic, IInitialRespositoryAPI initialRespositoryAPI) => {
            //    return initialRespositoryAPI.SearchInitial(topic);
            //});
            /// more registertaions

            return app;
        }

        public static WebApplication MapRandomEndPoints(this WebApplication app)
        {
            //app.MapPut("/initial", (IRandomRespositoryAPI randomRespositoryAPI) =>
            //{
            //    return initialRespositoryAPI.Random();
            //});
            /// more registertaions


            return app;
        }
    }
}
