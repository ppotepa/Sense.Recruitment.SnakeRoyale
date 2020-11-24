using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleLoggingService
{
    public class ConsoleLoggingService : ILoggingService
    {
        public void LogMessage(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {message}");
        }

        public void LogMessageAt(string message, int X, int Y)
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine(message);
        }
    }
}
