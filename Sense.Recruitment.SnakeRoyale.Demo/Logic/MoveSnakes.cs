using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class MoveSnakes : GameLogic
    {
        public MoveSnakes(ILoggingService loggingService, IGameEngineConfig config) : base(loggingService, config)
        {
        }
        public override void Apply(SimpleGameEngine engine)
        {
            List<GameObject> snakes = engine.GetObjectsByName("Snake").ToList();
            snakes.ForEach(snake => 
            {
                snake.Position += new Vector2D(1, 0);
            });
        }
    }
}
