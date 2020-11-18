using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public delegate void OnTickCompleted();
        public event OnTickCompleted TickCompleted;
    }
}
