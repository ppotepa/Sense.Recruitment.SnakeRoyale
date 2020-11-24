using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class AddSnakes : GameLogicBehaviour
    {
        private const int SnakeLimit = 1;
        protected new int Priority = 1;
        private static Random random = new Random();
        private static bool playerCreated = false;
        public AddSnakes(ILoggingService loggingService) : base(loggingService) { }       
        public override void ApplyTo(SimpleGameServer server)
        {
            while (server.GetCountByObjectName("Snake") < SnakeLimit)
            {
                GameObject snake = GameObject.Create
                (
                    objectName: "SomeSnake",
                    playable: false,
                    isSolid: true,
                    bitmapName: null,
                    position: new Vector2D(x: random.Next(0, 60), y: random.Next(0, 30)),
                    velocity: new Vector2D(x: 1, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake"
                );

                snake.ObjectProperties = new SnakeProperties(snake);
                server.AddObject(snake);
            }

            if (!playerCreated)
            {
                GameObject playerSnake = GameObject.Create
                (
                    objectName: "Player",
                    playable: true,
                    isSolid: true,
                    bitmapName: null,
                    position: new Vector2D(x: random.Next(0, 60), y: random.Next(0, 30)),
                    velocity: new Vector2D(x: 0, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake"                    
                );

                playerSnake.ObjectProperties = new SnakeProperties(playerSnake);
                server.AddObject(playerSnake);
                playerCreated = true;
            }
        }
    }
}
