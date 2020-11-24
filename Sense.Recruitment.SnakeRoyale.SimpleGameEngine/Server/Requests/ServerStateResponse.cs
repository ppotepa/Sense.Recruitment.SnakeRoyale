using System;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server.Requests
{
    public class ServerStateResponse
    {
        public ServerStateResponse()
        { 
        }

        public ServerStateResponse(IEnumerable<GameObject> gameObjects, IEnumerable<GameObject> removedObjects)
        {
            GameObjects = gameObjects ?? throw new ArgumentNullException(nameof(gameObjects));
            RemovedObjects = removedObjects ?? throw new ArgumentNullException(nameof(removedObjects));
        }

        public IEnumerable<GameObject> GameObjects { get; set; }
        public IEnumerable<GameObject> RemovedObjects { get; set; }
    }
}
