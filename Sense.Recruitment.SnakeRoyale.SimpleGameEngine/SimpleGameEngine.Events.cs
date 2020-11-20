using System;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public delegate void TickCompleted();
        public delegate void ObjectRemoved(GameObject @object);

        public event TickCompleted OnTickCompleted;
        public event ObjectRemoved OnObjectRemoved;

    }
}
