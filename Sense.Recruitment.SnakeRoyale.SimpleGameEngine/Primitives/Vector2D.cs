namespace Sense.Recruitment.SnakeRoyale.Engine.Primitives
{
    public struct Vector2D
    {
    
        public readonly int X, Y;

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
    }
}