using Castle.DynamicProxy;
using Playground.Application.Shared.AsyncLocals;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace Playground.Application.Shared
{
    public class LogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ExecutionTimeSinceLastLog", ExecutionTimeContext.GetFormattedExecutionTimeSinceLastLog()));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ExecutionTime", ExecutionTimeContext.GetFormattedExecutionTime()));            
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", UserAuthorizationContext.GetUserId()));
        }

        internal static void PushPropertyCustomerName(string name)
        {
            LogContext.PushProperty("CustomerData.Name", name);
        }
    }
}
