using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    class Snake
    {
        public int Length = 1;
        public LinkedList<GameObject> Tail = new LinkedList<GameObject>();
        public GameObject Head;
        public Snake(GameObject head)
        {
            Head = head;
        }
    }

    public class MoveSnakes : GameLogic
    {
        public MoveSnakes(ILoggingService loggingService) : base(loggingService) { }
        private static bool SnakesInitialized { get; set; }
        private static Dictionary<string, Snake> Snakes { get; set; } = new Dictionary<string, Snake>();
        private static List<Snake> SnakesList { get; set; }

        private static readonly Vector2D RIGHT = new Vector2D(1, 0);
        private static readonly Vector2D LEFT = new Vector2D(-1, 0);
        private static readonly Vector2D NORTH = new Vector2D(0, -1);
        private static readonly Vector2D SOUTH = new Vector2D(0, 1);

        private readonly Random random = new Random();

        private readonly Dictionary<Vector2D, Vector2D[]> AvailableDirections = new Dictionary<Vector2D, Vector2D[]>()
        {
            { RIGHT, new[]{ SOUTH, NORTH } },
            { LEFT, new []{ SOUTH, NORTH } },
            { SOUTH, new []{ LEFT, RIGHT} },
            { NORTH, new []{ LEFT, RIGHT} },
        };

        public override void Apply(SimpleGameEngine engine)
        {
            if (!SnakesInitialized)
            {
                engine.GetObjectsByName("Snake")
                    .ToList()
                    .ForEach(snake => Snakes.Add(snake.HashCode, new Snake(snake)));

                SnakesList = new List<Snake>(Snakes.Values.ToList());
                SnakesInitialized = true;
            }

            SnakesList.ForEach(snake =>
            {
                if (snake.Head.Position.X + snake.Head.Velocity.X < 119)
                {
                    while (snake.Tail.Count < snake.Length)
                    {
                        GameObject copy = snake.Head.Copy();
                        snake.Tail.AddFirst(copy);
                        engine.AddObject(copy);
                    }

                    engine.RemoveObject(snake.Tail.Last.Value);
                    snake.Tail.RemoveLast();
                    Vector2D newVelocity = new Vector2D(random.Next(-2, 2), random.Next(-2, 2));

                    if (AvailableDirections[snake.Head.Velocity].Contains(newVelocity))
                    {
                        snake.Head.Velocity = newVelocity;
                    }

                    snake.Head.Position += snake.Head.Velocity;

                    if (random.Next(1, 50) > 40) snake.Length++;

                }
            });
        }
    }
}
