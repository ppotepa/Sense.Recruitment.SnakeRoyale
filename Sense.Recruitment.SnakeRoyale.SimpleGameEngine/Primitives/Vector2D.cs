using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Primitives
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2D d && Equals(d);
        }

        public bool Equals(Vector2D other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public static bool operator ==(Vector2D a, Vector2D b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Vector2D a, Vector2D b) => a.X != b.X || a.Y != b.Y;

        public void Revert()
        {
            X *= -1;
            Y *= -1;
        }
    }
}