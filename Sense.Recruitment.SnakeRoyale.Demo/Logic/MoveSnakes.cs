using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class MoveSnakes : GameLogic
    {
        public MoveSnakes(ILoggingService loggingService) : base(loggingService) { }
        public override void Apply(SimpleGameEngine engine)
        {
            List<GameObject> snakes = engine.GetObjectsByName("Snake").ToList();
            snakes.ForEach(snake =>
            {
                if (snake.Position.X + snake.Velocity.X < 119)
                {
                    snake.Position += snake.Velocity;
                }
            });
        }
    }
}
