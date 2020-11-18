using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    public class AddSnakes : GameLogic
    {
        private const int SnakeLimit = 1;
        public AddSnakes(ILoggingService loggingService, IGameEngineConfig config) : base(loggingService, config)
        {

        }
        public override void Apply(SimpleGameEngine engine)
        {
            int currentSnakeCount = GameObject.GetCountByName("Snake");
            if (currentSnakeCount < SnakeLimit)
            {
                var currentAppleID = "PlayerSnake";
                GameObject snake = GameObject.Create
                (
                    objectName: currentAppleID,
                    playable: false,
                    isSolid: false,
                    bitmapName: null,
                    position: new Vector2D(x: 10, y: 10),
                    velocity: new Vector2D(x: 0, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake"
                );
                engine.AddObject(snake);
            }
        }
    }
}
