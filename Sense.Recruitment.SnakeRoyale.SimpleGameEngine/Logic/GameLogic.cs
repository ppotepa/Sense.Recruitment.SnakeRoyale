using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Engine.Logic
{
    public abstract class GameLogic : IGameLogic
    {
        protected readonly ILoggingService LoggingService;
        protected readonly IGameEngineConfig GameConfig;

        protected int ExecutionLimit = 0;
        protected int ExecutionTimes = 0;
        public int Priority = 0;

        protected GameLogic(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        public abstract void ApplyTo(SimpleGameEngine engine);
    }
}