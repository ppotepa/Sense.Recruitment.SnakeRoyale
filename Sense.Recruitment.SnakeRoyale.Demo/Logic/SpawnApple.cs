using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    [LogicPriority(0)]
    public class SpawnApple : GameLogicBehaviour
    {
        private const int AppleLimit = 50;
        protected int Priority = 100;
        private static Random random = new Random();
        public SpawnApple(ILoggingService loggingService) : base(loggingService) { }
        public override void ApplyTo(SimpleGameServer server)
        {

            while (server.GetCountByObjectName("Apple") < AppleLimit)
            {
                Vector2D applePosition = new Vector2D(x: random.Next(0, 60) * 32, y: random.Next(0, 32) * 32);
                LoggingService.LogMessage("Spawning Apple(s).");
                if (!server.GetObjectAt(applePosition).Any())
                {
                    GameObject apple = GameObject.Create
                    (
                        objectName: "Test",
                        playable: false,
                        isSolid: false,
                        bitmapName: "apple.png",
                        position: applePosition,
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
}
