using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class DetectCollision : GameLogicBehaviour
    {
        public DetectCollision(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; } = false;
        private static List<GameObject> Snakes { get; set; } = new List<GameObject>();
     
        public override void ApplyTo(SimpleGameServer server)
        {
            if (!SnakesInitialized)
            {
                Snakes = server.GetObjectsByName("Snake").ToList();
                SnakesInitialized = true;
            }
            
            Snakes.ForEach(snake => 
            {
                SnakeProperties props = (SnakeProperties) snake.ObjectProperties;
                bool colided = server.GetObjectAt(props.Head.Position).Any(obj => obj.IsSolid && obj != snake);
                if (colided)
                {
                    server.RemoveObject(snake);
                }
            });
        }
    }
}
