using Serilog;
using System.Diagnostics;

namespace Playground.Application.Shared.AsyncLocals
{
    public static class ExecutionTimeContext
    {
        private static readonly AsyncLocal<Stopwatch> _totalLogTime = new();
        private static readonly AsyncLocal<Stopwatch> _lastLogTime = new();

        public static void Start()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            _totalLogTime.Value = stopwatch;
            _lastLogTime.Value = new Stopwatch();
        }

        public static string GetFormattedExecutionTime()
        {
            if (_totalLogTime.Value == null)
                return FormattedTimeResult(TimeSpan.Zero);

            TimeSpan totalLogTime = _totalLogTime.Value.Elapsed;

            return FormattedTimeResult(totalLogTime);
        }

        public static string GetFormattedExecutionTimeSinceLastLog()
        {
            if (_lastLogTime.Value == null)
                return FormattedTimeResult(TimeSpan.Zero);

            TimeSpan lastLogTime = _lastLogTime.Value.Elapsed;

            _lastLogTime.Value.Reset();
            _lastLogTime.Value.Start();

            return FormattedTimeResult(lastLogTime);
        }

        private static string FormattedTimeResult(TimeSpan timeSpan)
        {
            return $"{timeSpan.Minutes:D2}m {timeSpan.Seconds:D2}s {timeSpan.Milliseconds:D3}ms {timeSpan.Ticks % TimeSpan.TicksPerMillisecond:D4}ns";
        }
    }
}
