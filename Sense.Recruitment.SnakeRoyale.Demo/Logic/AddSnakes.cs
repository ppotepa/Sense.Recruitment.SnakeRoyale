using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class AddSnakes : GameLogic
    {
        private const int SnakeLimit = 1;
        protected new int Priority = 100;

        public AddSnakes(ILoggingService loggingService) : base(loggingService) { }       
        public override void ApplyTo(SimpleGameEngine engine)
        {
            int currentSnakeCount = engine.GetCountByObjectName("Snake");
            if (currentSnakeCount < SnakeLimit)
            {
                var currentAppleID = "PlayerSnake";
                GameObject snake = GameObject.Create
                (
                    objectName: "PlayerSnake",
                    playable: false,
                    isSolid: true,
                    bitmapName: null,
                    position: new Vector2D(x: 40, y: 20),
                    velocity: new Vector2D(x: 1, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake"
                );

                snake.ObjectProperties = new SnakeProperties(snake);
                engine.AddObject(snake);
            }
        }
    }
}
