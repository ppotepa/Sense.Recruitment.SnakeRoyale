using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    [LogicPriority(3)]
    public class MoveSnakes : GameLogicBehaviour
    {
        public MoveSnakes(ILoggingService loggingService) : base(loggingService) { }
        private static List<GameObject> Snakes { get; set; }

        private static readonly Vector2D EAST = new Vector2D(32, 0);
        private static readonly Vector2D WEST = new Vector2D(-32, 0);
        private static readonly Vector2D NORTH = new Vector2D(0, -32);
        private static readonly Vector2D SOUTH = new Vector2D(0, 32);
        private static readonly Vector2D NONE = new Vector2D(0, 0);

        private readonly Random random = new Random();

        private readonly Dictionary<Vector2D, Vector2D[]> AvailableDirections = new Dictionary<Vector2D, Vector2D[]>()
        {
            { EAST,     new[] { SOUTH,  NORTH } },
            { WEST,     new[] { SOUTH,  NORTH } },
            { SOUTH,    new[] { WEST,   EAST} },
            { NORTH,    new[] { WEST,   EAST} },
            { NONE,     new[] { WEST,   EAST, SOUTH, NORTH } },
        };

        public override void ApplyTo(SimpleGameServer server)
        {
            Snakes = server
                    .GetObjectsByName("Snake")
                    .Where(@object => @object.ObjectName == "SnakeHead")
                    .ToList();

            Snakes.ForEach(snake =>
            {
                server.LoggingService.LogMessage("Moving snake");
                SnakeProperties snakeProperties = (SnakeProperties) snake.ObjectProperties;

                if (snakeProperties.Tail.Any())
                {
                    server.RemoveObject(snakeProperties.Tail.Last.Value);
                    snakeProperties.Tail.RemoveLast();
                }

                while (snakeProperties.Tail.Count < snakeProperties.Length)
                {
                    GameObject copy = snakeProperties.Head.Copy();
                    snakeProperties.Tail.AddFirst(copy);
                    server.AddObject(copy);
                }
                if (!snake.Playable)
                {
                    Vector2D newVelocity = new Vector2D(random.Next(-2, 2) * 32, random.Next(-2, 2) * 32);
                    if (AvailableDirections[snakeProperties.Head.Velocity].Contains(newVelocity))
                    {
                        snakeProperties.Head.Velocity = newVelocity;
                    }
                }

                snakeProperties.Head.Position += snakeProperties.Head.Velocity;
                snake.Rotation += 0.10;
            });
        }
    }
}
