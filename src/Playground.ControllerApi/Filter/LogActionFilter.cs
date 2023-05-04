using Microsoft.AspNetCore.Mvc.Filters;
using Playground.Models;
using Serilog.Context;

namespace Playground.Filter
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
