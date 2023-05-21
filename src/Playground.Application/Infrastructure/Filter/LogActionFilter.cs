using Microsoft.AspNetCore.Mvc.Filters;
using Playground.Application.Shared.AsyncLocals;
using Serilog.Context;

namespace Playground.Application.Infrastructure.Filter
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var correlationId = CorrelationContext.GetCorrelationId();

            LogContext.PushProperty("CorrelationId", correlationId);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
