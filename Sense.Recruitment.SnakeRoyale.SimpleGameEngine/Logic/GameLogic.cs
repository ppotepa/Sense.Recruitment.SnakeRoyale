using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Engine.Logic
{
    public abstract class GameLogicBehaviour : IGameLogic
    {
        protected readonly ILoggingService LoggingService;
        protected readonly IGameEngineConfig GameConfig;

        protected int ExecutionLimit = 0;
        protected int ExecutionTimes = 0;

        protected GameLogicBehaviour(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        public abstract void ApplyTo(SimpleGameServer server);
    }
}