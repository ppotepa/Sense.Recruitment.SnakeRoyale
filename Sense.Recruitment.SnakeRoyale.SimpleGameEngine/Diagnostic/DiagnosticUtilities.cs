using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Diagnostics;

namespace Sense.Recruitment.SnakeRoyale.Engine.Diagnostic
{
    public static class DiagnosticUtilities
    {
        public static long MeasureExecutionTime(Action action, ILoggingService loggingService, bool silent = false)
        {
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action.Invoke();
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
    }
}