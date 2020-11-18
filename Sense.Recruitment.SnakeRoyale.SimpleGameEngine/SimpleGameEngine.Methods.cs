using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public GameObject GetObject(string objectHash) => GameObjects[objectHash];
        public GameObject AddObject(GameObject gameObject) => GameObjects[gameObject.HashCode] = gameObject;
        public IEnumerable<GameObject> GetObjectsByName(string objectName) => GameObjects.Values.Where(obj => obj.ObjectTypeName == objectName);
        public IEnumerable<GameObject> GetObjectAt(Vector2D position) => GameObjects.Values.Where(obj => obj.Position == position);
       
    }
}
