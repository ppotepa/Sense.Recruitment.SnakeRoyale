using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Handlers
{
    //to be moved to sort of a different logic
    static class ConnectionHandlers
    {
        private static readonly Random random = new Random();
        public static void CreatePlayer(SimpleGameServer server, Engine.Network.Client client)
        {
            GameObject player = GameObject.Create
                (
                    objectName: "Player",
                    playable: true,
                    isSolid: false,
                    bitmapName: "snake.png",
                    position: new Vector2D(x:1000, y:800),
                    velocity: new Vector2D(x: 32, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake",
                    owner: client
                );
            server.AddObject(player);
        }
    }
}
