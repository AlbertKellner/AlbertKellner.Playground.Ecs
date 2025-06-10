using Microsoft.AspNetCore.Http;
using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Infrastructure.Middleware
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecutionTimeMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ExecutionTimeContext.Start();

            context.Response.OnStarting(() =>
            {
                context.Response.Headers["Execution-Time"] = ExecutionTimeContext.GetFormattedExecutionTime();
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
