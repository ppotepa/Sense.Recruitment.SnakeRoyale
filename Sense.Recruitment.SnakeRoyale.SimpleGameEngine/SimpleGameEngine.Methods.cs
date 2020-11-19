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
        public SimpleGameEngine UseDefaultLogic() { usingDefaultLogic = true; return this; }
        public SimpleGameEngine LoadAssets(string configFileName = "game.assets.json") { return this; }
        public SimpleGameEngine LoadStages(string configFileName = "game.stages.json") { return this; }
        public SimpleGameEngine LoadConfiguration(string configFileName = "engine.config.json") { return this; }
        public SimpleGameEngine LoadDefaultObjects(string configFileName = "game.objects.json") { return this; }
    }
}
