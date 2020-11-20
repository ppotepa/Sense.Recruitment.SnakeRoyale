using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
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
            this.Snake = snake;
            this.Apple = apple;
        }

        public readonly GameObject Snake;
        public readonly GameObject Apple;
    }
    public class PickUpAnApple : GameLogic
    {
        public PickUpAnApple(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; } = false;
        private static List<GameObject> Snakes { get; set; } = new List<GameObject>();
        private static List<GameObject> Apples { get; set; }  = new List<GameObject>();

        public delegate void ApplePickedUp(EventArgs ApplePickedUpEventArgs);
        public event ApplePickedUp OnApplePickedUp;

        public override void ApplyTo(SimpleGameEngine engine)
        {
            if (!SnakesInitialized)
            {
                Snakes = engine.GetObjectsByName("Snake")
                    .ToList();
                SnakesInitialized = true;
            }

            Apples = engine.GetObjectsByName("Apple").ToList();
            Snakes.ForEach(snake => 
            {
                var props = (SnakeProperties)snake.ObjectProperties;
                Apples.ForEach(apple => 
                {
                    if (apple.Position == props.Head.Position) 
                    {
                        engine.RemoveObject(apple);
                        props.Length += 1;
                    }
                });
            });
        }
    }
}
