namespace Sense.Recruitment.SnakeRoyale.Engine.Services
{
    public interface ILoggingService
    {
        void LogMessage(string message);
        void LogMessageAt(string message, int X, int Y);
    }
}
