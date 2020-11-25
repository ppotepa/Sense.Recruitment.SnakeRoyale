using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic
{
    [LogicPriority(1)]
    public class SpawnObstacles : GameLogicBehaviour
    {
        private const int ObstaclesCount = 50;
        private Random random = new Random();
        public SpawnObstacles(ILoggingService loggingService) : base(loggingService) { }
        public override void ApplyTo(SimpleGameServer server)
        {            
            while (server.GetCountByObjectName("Obstacle") < ObstaclesCount)
            {
                LoggingService.LogMessage("Spawning Obstacles.");
                Vector2D obstaclePosition = new Vector2D(x: random.Next(0, 60) * 32, y: random.Next(0, 32) * 32);
                if (!server.GetObjectAt(obstaclePosition).Any())
                {
                    GameObject apple = GameObject.Create
                    (
                        objectName: "Obstacle",
                        playable: false,
                        isSolid: true,
                        bitmapName: "obstacle.png",
                        position: obstaclePosition,
                        velocity: new Vector2D(x: 0, y: 0),
                        roration: 0,
                        scale: 1,
                        objectTypeName: "Obstacle",
                        owner: null,
                        properties: null
                    );
                    server.AddObject(apple);
                }
                else
                { 

                }

            }
        }
    }
}
