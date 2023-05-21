using System.Diagnostics;

namespace Playground.Application.Shared.AsyncLocals
{
    public static class ExecutionTimeContext
    {
        private static readonly AsyncLocal<Stopwatch> _stopwatch = new();

        public static void Start()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            _stopwatch.Value = stopwatch;
        }

        public static string GetFormattedExecutionTime()
        {
            if (_stopwatch.Value == null)
                return "";

            TimeSpan timeStop = _stopwatch.Value.Elapsed;

            return $"{timeStop.Minutes:D2}m {timeStop.Seconds:D2}s {timeStop.Milliseconds:D3}ms {timeStop.Ticks % TimeSpan.TicksPerMillisecond:D4}ns";
        }
    }
}
