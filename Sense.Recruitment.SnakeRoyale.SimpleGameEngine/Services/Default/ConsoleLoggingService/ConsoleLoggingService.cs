using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleLoggingService
{
    public class ConsoleLoggingService : ILoggingService
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
