using System;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.Tools
{
    public static class RandomTools
    {
        private static Random random = new Random();
        public static string CreateHashCode(int length) => string.Join(string.Empty, Enumerable.Range(0, length).Select(i => (char)random.Next(65, 90)));
    };
}
