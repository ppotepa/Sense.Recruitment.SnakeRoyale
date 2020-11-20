using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class DetectCollision : GameLogic
    {
        public DetectCollision(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; } = false;
        private static List<GameObject> Snakes { get; set; } = new List<GameObject>();
        private static List<GameObject> Apples { get; set; }  = new List<GameObject>();
        public override void ApplyTo(SimpleGameEngine engine)
        {
            if (!SnakesInitialized)
            {
                Snakes = engine.GetObjectsByName("Snake").ToList();
                SnakesInitialized = true;
            }
            
            Snakes.ForEach(snake => 
            {
                SnakeProperties props = (SnakeProperties) snake.ObjectProperties;
                bool colided = engine.GetObjectAt(props.Head.Position).Any(obj => obj.IsSolid && obj != snake);
                if (colided)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
            });
        }
    }
}
