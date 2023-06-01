using Microsoft.Extensions.Logging;
using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Infrastructure.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogTroubleshooting(this ILogger logger, string message)
        {
            Guid correlationIdToDebud = new("550e8400-1234-5678-9999-999900000001");
            var correlationId = CorrelationContext.GetCorrelationId();

            if (correlationId == correlationIdToDebud)
            {
                logger.LogInformation(message);
            }
        }
    }
}
