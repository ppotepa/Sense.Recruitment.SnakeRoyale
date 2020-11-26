using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    [LogicPriority(4)]
    public class AddSnakes : GameLogicBehaviour
    {
        private const int SnakeLimit = 300;
        private Random random = new Random();   
        public AddSnakes(ILoggingService loggingService) : base(loggingService) { }       
        public override void ApplyTo(SimpleGameServer server)
        {
            while (server.GetCountByObjectName("Snake") < SnakeLimit)
            {
                GameObject snake = GameObject.Create
                (
                    objectName: "SnakeHead",
                    playable: false,
                    isSolid: true,
                    bitmapName: "snake.png",
                    position: new Vector2D(x: random.Next(0, 60)*32, y: random.Next(0, 32)*32),
                    velocity: new Vector2D(x: 32, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake",
                    owner:null,
                    properties:null
                );

                snake.ObjectProperties = new SnakeProperties(snake);
                server.AddObject(snake);
            }
        }
    }
}
