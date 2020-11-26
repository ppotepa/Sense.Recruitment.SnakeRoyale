using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    [LogicPriority(5)]
    public class DetectCollision : GameLogicBehaviour
    {
        public DetectCollision(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; } = false;
        private static List<GameObject> Snakes { get; set; } = new List<GameObject>();
     
        public override void ApplyTo(SimpleGameServer server)
        {
            Snakes = server
                .GetObjectsByName("Snake")
                .Where(@object => @object.ObjectName == "SnakeHead")
                .ToList();

            Snakes.ForEach(snake =>
            {
                SnakeProperties props = (SnakeProperties)snake.ObjectProperties;
                bool colided = server.GetObjectAt(props.Head.Position).Any(obj => obj.IsSolid && obj != snake);
                if (colided)
                {
                    //LoggingService.LogMessage($"Snake {snake.Owner.ClientHashCode} colided with an object.");

                    props.Tail.ToList().ForEach(tailBit => server.RemoveObject(tailBit));
                    server.RemoveObject(snake);
                }
            });
        }
    }
}
