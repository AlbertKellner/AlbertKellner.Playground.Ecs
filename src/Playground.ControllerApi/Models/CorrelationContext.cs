using Serilog.Context;

namespace Playground.Models
{
    public static class CorrelationContext
    {
        private static readonly AsyncLocal<Guid> _correlationId = new();

        public static Guid GetCorrelationId()
        {
            return _correlationId.Value;
        }

        public static void SetCorrelationId(Guid correlationId)
        {
            _correlationId.Value = correlationId;

            LogContext.PushProperty("CorrelationId", correlationId);
        }
    }
}
