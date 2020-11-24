using Sense.Recruitment.SnakeRoyale.Engine.Network;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    public partial class SimpleGameServer
    {
        public delegate void TickCompleted();
        public delegate void NewClientRegistred(SimpleGameServer server, Client newClient);

        public delegate void ObjectRemoved(GameObject @object);
        public delegate void ObjectsRemoved(GameObject[] @object);

        public event TickCompleted OnTickCompleted;
        public event ObjectRemoved OnObjectRemoved;

        public event NewClientRegistred OnNewClientRegistred;
    }
}
