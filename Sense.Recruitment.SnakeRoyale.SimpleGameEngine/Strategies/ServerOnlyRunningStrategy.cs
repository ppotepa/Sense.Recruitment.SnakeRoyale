using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Strategies
{
    public class ServerOnlyRunningStrategy : IRunningStrategy
    {
        public ServerOnlyRunningStrategy(SimpleGameEngine engine, IEnumerable<GameLogicBehaviour> behavioues) : base(engine)
        {
        }
    }
}