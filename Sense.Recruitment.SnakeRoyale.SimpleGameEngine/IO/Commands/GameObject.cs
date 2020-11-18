using Sense.Recruitment.SnakeRoyale.Engine.Primitives;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class GameObject
    {
        public readonly Vector2D Position;
        public GameObject(Vector2D position, string objectType)
        {
            Position = position;
        }
    }
}