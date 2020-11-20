namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public delegate void OnTickCompleted();
        public event OnTickCompleted TickCompleted;
    }
}
