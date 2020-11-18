using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class SpawnApple : GameLogic
    {
        private const int AppleLimit = 100;
        public SpawnApple(ILoggingService loggingService, IGameEngineConfig gameConfig) : base(loggingService, gameConfig) { }

        public override void Apply(SimpleGameEngine engine)
        {
            var currentAppleCount = GameObject.GetCountByName("Apple");
            if (currentAppleCount < AppleLimit)
            {
                var currentAppleID = "Apple" + currentAppleCount;
                //LoggingService.LogMessage("Adding an Apple " + currentAppleID);
                GameObject apple = GameObject.Create
                (
                    objectName: currentAppleID,
                    playable: false,
                    isSolid: false,
                    bitmapName: null,
                    position: new Vector2D(x: 20, y: 20),
                    velocity: new Vector2D(x: 0, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Apple"
                );
                engine.AddObject(apple);
            }
        }
    }
}
