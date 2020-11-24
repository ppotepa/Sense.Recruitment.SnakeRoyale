using Newtonsoft.Json;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public class GameObject
    {
        private static readonly Dictionary<string, int> CountByName = new Dictionary<string,int>();
        public static int GetObjectsCount() => CountByName.Sum(key => key.Value);
        public static int GetCountByObjectName(string objectTypeName) => CountByName.ContainsKey(objectTypeName) ? CountByName[objectTypeName] : 0;      

        public static GameObject Create(string objectName, Vector2D position, Vector2D velocity, bool playable,
                                        bool isSolid, string bitmapName, double roration, double scale,
                                        string objectTypeName, Client owner)
        {
            if (!CountByName.ContainsKey(objectTypeName))
            {
                CountByName[objectTypeName] = 1;
            }
            else CountByName[objectTypeName]++;

            return new GameObject()
            {
                Playable = playable,
                IsSolid = isSolid,
                BitmapName = bitmapName ?? "Unnamed",
                Position = position,
                Velocity = velocity,
                Rotation = roration,
                Scale = scale,
                ObjectTypeName = objectTypeName ?? "Unnamed",
                Owner = owner,
            };
        }

        public GameObject() { }
        public bool Playable { get; set; }
        public string ObjectName { get; set; }
        public bool IsSolid { get; set; }
        public string BitmapName { get; set; }
        public Vector2D Position { get; set; }
        public Vector2D Velocity { get; set; }
        public double Rotation { get; set; }
        public double Scale { get; set; }
        public string ObjectTypeName { get; set; }

        public readonly string HashCode = RandomTools.CreateHashCode(10);
        private Client Owner;
        [JsonIgnore]
        public ObjectProperties ObjectProperties { get; set; }

        public GameObject Copy() => Create(ObjectName, Position, Velocity, Playable, IsSolid, BitmapName, Rotation, Scale, ObjectTypeName, Owner);
        public static bool Remove(GameObject @object)
        {
            CountByName[@object.ObjectTypeName]--;
            return true;
        }
    }
}
