using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class AddSnakes : GameLogic
    {
        private const int SnakeLimit = 1;
        public AddSnakes(ILoggingService loggingService) : base(loggingService) { }       
        public override void Apply(SimpleGameEngine engine)
        {
            int currentSnakeCount = GameObject.GetCountByObjectName("Snake");
            if (currentSnakeCount < SnakeLimit)
            {
                var currentAppleID = "PlayerSnake";
                GameObject snake = GameObject.Create
                (
                    objectName: "PlayerSnake2",
                    playable: false,
                    isSolid: false,
                    bitmapName: null,
                    position: new Vector2D(x: 40, y: 20),
                    velocity: new Vector2D(x: 1, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake"
                );
                engine.AddObject(snake);
            }
        }
    }
}
