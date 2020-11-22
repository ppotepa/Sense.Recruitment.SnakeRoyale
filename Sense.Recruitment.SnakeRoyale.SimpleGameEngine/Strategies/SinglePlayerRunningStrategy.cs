using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Strategies
{
    public class SinglePlayerRunningStrategy : IRunningStrategy
    {
        public SinglePlayerRunningStrategy(SimpleGameEngine engine, IEnumerable<GameLogicBehaviour> behavioues) : base(engine)
        {

        }
    }
}