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
        public int Priority { get; set; } = 0;
        private bool Completed { get; set; } = false;
        protected GameLogicBehaviour(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        public abstract void ApplyTo(SimpleGameServer server);
    }
}