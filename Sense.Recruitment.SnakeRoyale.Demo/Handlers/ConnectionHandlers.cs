using Sense.Recruitment.SnakeRoyale.Demo.Logic.Models;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
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
                    objectName: "SnakeHead",
                    playable: true,
                    isSolid: true,
                    bitmapName: "snake.png",
                    position: new Vector2D(x:random.Next(0, 30) * 32, y: random.Next(0, 30) * 32),
                    velocity: new Vector2D(x: 32, y: 0),
                    roration: 0,
                    scale: 1,
                    objectTypeName: "Snake",
                    owner: client,
                    properties:null
                );

            player.ObjectProperties = new SnakeProperties(player);
            server.AddObject(player);
        }
    }
}
