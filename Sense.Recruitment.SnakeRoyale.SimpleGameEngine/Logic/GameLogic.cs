using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Engine.Logic
{
    public abstract class GameLogic : IGameLogic
    {
        protected readonly ILoggingService LoggingService;
        protected readonly IGameEngineConfig GameConfig;
        protected GameLogic(ILoggingService loggingService)
        {
            LoggingService = loggingService;
        }

        public abstract void Apply(SimpleGameEngine engine);
    }
}