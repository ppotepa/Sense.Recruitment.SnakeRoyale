﻿using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
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
    public class MoveSnakes : GameLogicBehaviour
    {
        public MoveSnakes(ILoggingService loggingService) : base(loggingService) { }

        private static Dictionary<string, GameObject> Snakes { get; set; } = new Dictionary<string, GameObject>();
        private static List<SnakeProperties> SnakesList { get; set; }

        private static readonly Vector2D EAST = new Vector2D(1, 0);
        private static readonly Vector2D WEST = new Vector2D(-1, 0);
        private static readonly Vector2D NORTH = new Vector2D(0, -1);
        private static readonly Vector2D SOUTH = new Vector2D(0, 1);
        private static readonly Vector2D WORLD_BOUNDARIES = new Vector2D(100, 60);

        private readonly Random random = new Random();

        private readonly Dictionary<Vector2D, Vector2D[]> AvailableDirections = new Dictionary<Vector2D, Vector2D[]>()
        {
            { EAST, new[] { SOUTH, NORTH } },
            { WEST,  new[] { SOUTH, NORTH } },
            { SOUTH, new[] { WEST, EAST} },
            { NORTH, new[] { WEST, EAST} },
        };

        public override void ApplyTo(SimpleGameServer server)
        {
            if (Snakes is null) 
                Snakes = server.GetObjectsByName("Snake").ToDictionary(snake => snake.HashCode, snake => snake);
      
            Snakes.Values.ToList().ForEach(snake =>
            {
                SnakeProperties snakeProperties = (SnakeProperties) snake.ObjectProperties;
                if (snakeProperties.Head.Position.X + snakeProperties.Head.Velocity.X < 119)
                {
                    while (snakeProperties.Tail.Count < snakeProperties.Length)
                    {
                        GameObject copy = snakeProperties.Head.Copy();
                        snakeProperties.Tail.AddFirst(copy);
                        server.AddObject(copy);
                    }

                    server.RemoveObject(snakeProperties.Tail.Last.Value);
                    snakeProperties.Tail.RemoveLast(); 
                    Vector2D newVelocity = new Vector2D(random.Next(-2, 2), random.Next(-2, 2));

                    if (AvailableDirections[snakeProperties.Head.Velocity].Contains(newVelocity))
                    {
                        snakeProperties.Head.Velocity = newVelocity;
                    }

                    snakeProperties.Head.Position += snakeProperties.Head.Velocity;
                }
            });
        }
    }
}
