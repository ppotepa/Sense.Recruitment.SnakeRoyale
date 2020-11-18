using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Diagnostics;

namespace Sense.Recruitment.SnakeRoyale.Engine.Diagnostic
{
    public static class DiagnosticUtilities
    {
        public static long MeasureExecutionTime(Action action, ILoggingService loggingService, bool silent = false)
        {
            if(!silent) loggingService.LogMessage($"Starting execution of [{action.Method.Name}]");
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action.Invoke();
            stopWatch.Stop();
            if (!silent) loggingService.LogMessage($"Execution of [{action.Method.Name}] took [{stopWatch.ElapsedMilliseconds}ms]");
            return stopWatch.ElapsedMilliseconds;
        }
    }
}