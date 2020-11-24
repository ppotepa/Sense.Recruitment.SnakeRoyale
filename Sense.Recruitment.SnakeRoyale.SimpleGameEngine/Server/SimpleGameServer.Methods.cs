using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    public partial class SimpleGameServer
    {
        public GameObject GetObject(string objectHash) => GameObjects[objectHash];
        public GameObject AddObject(GameObject gameObject) => GameObjects[gameObject.HashCode] = gameObject;
        public IEnumerable<GameObject> GetObjectsByName(string objectName) => GameObjects.Values.Where(obj => obj.ObjectTypeName == objectName);
        internal GameObject GetObjectByTypeHashCode(string hashCode) => GameObjects.ContainsKey(hashCode) ? GameObjects[hashCode] : null;

        public TGameObjectType[] GetObjectsByType<TGameObjectType>() where TGameObjectType : GameObject
            => (TGameObjectType[])GameObjects.Values.Where(obj => obj.GetType() == typeof(TGameObjectType)).ToArray();
        public IEnumerable<GameObject> GetObjectAt(Vector2D position) => GameObjects.Values.Where(obj => obj.Position == position);
        public int GetCountByObjectName(string objectName) => GameObjects.Count(e => e.Value.ObjectTypeName == objectName);

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

        internal bool UseDefaultLogic { get; set; } =true;
        internal void AddToQueue(ICommand command) => CommandStack.Push(command);
        public bool IsWebSocketRunning => this.WebSocketServer.IsListening;
       
    }
}
