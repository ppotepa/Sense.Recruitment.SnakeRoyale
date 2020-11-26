using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class ApplePickedUpEventArgs : EventArgs
    {
        public ApplePickedUpEventArgs(GameObject snake, GameObject apple)
        {
            Snake = snake;
            Apple = apple;
        }

        public readonly GameObject Snake;
        public readonly GameObject Apple;
    }
    public class PickUpAnApple : GameLogicBehaviour
    {
        public PickUpAnApple(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; } = false;
        private static List<GameObject> Snakes { get; set; } = new List<GameObject>();
        private static List<GameObject> Apples { get; set; }  = new List<GameObject>();
        public override void ApplyTo(SimpleGameServer server)
        {
            Snakes = server.GetObjectsByName("Snake").Where(@object => @object.ObjectName == "SnakeHead").ToList();
            Apples = server.GetObjectsByName("Apple").ToList();

            Snakes.ForEach(snake => 
            {
                SnakeProperties properties = (SnakeProperties) snake.ObjectProperties;
                Apples.ForEach(apple => 
                {
                    if (apple.Position == properties.Head.Position) 
                    {
                        
                        //LoggingService.LogMessage($"Player picked up an Apple.{snake.Owner.ClientHashCode}");
                        server.RemoveObject(apple);
                        properties.Length += 1;
                        properties.Score += 1;
                        if (server.TickInterval - 20 > 0) 
                        {
                            server.SetTickInterval(server.TickInterval - 20);
                        }
                    }
                });
            });
        }
    }
}
