using Playground.Application.Shared.AsyncLocals;
using Serilog.Core;
using Serilog.Events;

namespace Playground.Application.Shared
{
    public class ExecutionTimeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ExecutionTime", ExecutionTimeContext.GetFormattedExecutionTime()));
        }
    }
}
