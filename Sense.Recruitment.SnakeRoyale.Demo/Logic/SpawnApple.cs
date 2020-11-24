using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class SpawnApple : GameLogicBehaviour
    {
        private const int AppleLimit = 250;
        private static Random random = new Random();
        public SpawnApple(ILoggingService loggingService) : base(loggingService) { }
        public override void ApplyTo(SimpleGameServer server)
        {
            while (server.GetCountByObjectName("Apple") < AppleLimit)
            {
                GameObject apple = GameObject.Create
                (
                    objectName: "Test",
                    playable: false,
                    isSolid: false,
                    bitmapName: "apple.png",
                    position: new Vector2D(x: random.Next(0, 60) * 32, y: random.Next(0, 32) * 32),
                    velocity: new Vector2D(x: 0, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Apple",
                    owner: null,
                    properties: new AppleProperties()
                );
                server.AddObject(apple);
            }
        }
    }
}
