using Microsoft.Extensions.Logging;
using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Infrastructure.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogTroubleshooting(this ILogger logger, string message)
        {
            Guid correlationIdToDebug = new("550e8400-1234-5678-9999-999900000001"); //TODO: Take from FeatureManager
            var correlationId = CorrelationContext.GetCorrelationId();

            if (correlationId == correlationIdToDebug)
            {
                logger.LogInformation(message);
            }
        }
    }
}
