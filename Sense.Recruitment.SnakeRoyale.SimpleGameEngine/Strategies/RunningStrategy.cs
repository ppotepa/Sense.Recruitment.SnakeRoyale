using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Strategies
{
    
    public abstract class RunningStrategy
    {
        protected RunningStrategy(SimpleGameServer server, IEnumerable<GameLogicBehaviour> behavioues)
        {

        }
    }
}