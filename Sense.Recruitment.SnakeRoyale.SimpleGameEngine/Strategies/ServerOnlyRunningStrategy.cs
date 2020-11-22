using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Strategies
{
    public class ServerOnlyRunningStrategy : RunningStrategy
    {
        public ServerOnlyRunningStrategy(SimpleGameServer server, IEnumerable<GameLogicBehaviour> behavioues) : base(server, behavioues)
        {
        }
    }
}