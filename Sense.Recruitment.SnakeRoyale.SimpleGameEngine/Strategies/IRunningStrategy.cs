using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Strategies
{
    public interface IRunningStrategy
    {
    }
    public abstract class RunningStrategy : IRunningStrategy
    {
        public RunningStrategy(SimpleGameEngine engine, IEnumerable<GameLogicBehaviour> behavioues)
        {

        }
    }
}