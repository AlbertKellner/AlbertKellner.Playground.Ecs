using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Playground.Application.Shared.AsyncLocals;
using System.Text.Json;

namespace Playground.Application.Infrastructure.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(
            RequestDelegate next,
            ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Guid correlationId;

            if (context.Request.Headers.TryGetValue("CorrelationId", out var correlationIdValue))
            {
                if (Guid.TryParse(correlationIdValue, out correlationId))
                {
                    CorrelationContext.SetCorrelationId(correlationId);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var errorResponse = new { error = $"CorrelationId inválido. CorrelationId fornecido: {correlationIdValue}. Ele deve ser um GUID válido." };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

                    _logger.LogWarning("[CorrelationIdMiddleware] CorrelationId inválido. CorrelationId fornecido: {@CorrelationId}",
                        correlationIdValue.ToString());

                    return;
                }
            }
            else
            {
                correlationId = Guid.NewGuid();

                CorrelationContext.SetCorrelationId(correlationId);

                _logger.LogWarning("[CorrelationIdMiddleware] CorrelationId ausente. Gerando novo CorrelationId: {@CorrelationId}", correlationId);
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("CorrelationId", correlationId.ToString());

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
