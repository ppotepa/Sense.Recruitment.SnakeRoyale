using Sense.Recruitment.SnakeRoyale.Engine.IO;
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
        public TGameObjectType[] GetObjectsByType<TGameObjectType>() where TGameObjectType : GameObject
            => (TGameObjectType[]) GameObjects.Values.Where(obj => obj.GetType() == typeof(TGameObjectType)).ToArray();
        public IEnumerable<GameObject> GetObjectAt(Vector2D position) => GameObjects.Values.Where(obj => obj.Position == position);
        public SimpleGameEngine UseDefaultLogic() { usingDefaultLogic = true; return this; }
        public SimpleGameEngine LoadAssets(string configFileName = "game.assets.json") { return this; }
        public SimpleGameEngine LoadStages(string configFileName = "game.stages.json") { return this; }
        public SimpleGameEngine LoadConfiguration(string configFileName = "engine.config.json") { return this; }
        public SimpleGameEngine LoadDefaultObjects(string configFileName = "game.objects.json") { return this; }
        public int GetCountByObjectName(string objectName) => GameObjects.Where(e => e.Value.ObjectTypeName == objectName).Count();
       
        public void AddCommandToQueue(ICommand command) => CommandQueue.Add(command);
        public void RemoveObjectAt(Vector2D position, GameObject @object) 
        {
            GameObject target = GameObjects.First(g => g.Value.Position == position).Value;
            GameObjects.Remove(target.HashCode);
        }

        public bool RemoveObject(GameObject @object) 
        {
            GameObjects.Remove(@object.HashCode);
            OnObjectRemoved?.Invoke(@object);
            return true;
        }
        
    }
}
