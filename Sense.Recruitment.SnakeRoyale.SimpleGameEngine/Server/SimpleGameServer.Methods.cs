﻿using Newtonsoft.Json;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    public partial class SimpleGameServer
    {
        public GameObject GetObject(string objectHash) => GameObjects[objectHash];
        public GameObject AddObject(GameObject gameObject) => GameObjects[gameObject.HashCode] = gameObject;
        public IEnumerable<GameObject> GetObjectsByName(string objectName) => GameObjects.Values
            .Where(obj => obj.ObjectTypeName.Equals(objectName, System.StringComparison.InvariantCultureIgnoreCase));

        internal GameObject GetObjectByTypeHashCode(string hashCode) => GameObjects
            .ContainsKey(hashCode) 
            ? GameObjects[hashCode] 
            : null;

        public TGameObjectType[] GetObjectsByType<TGameObjectType>() where TGameObjectType : GameObject
            => (TGameObjectType[])GameObjects.Values
            .Where(obj => obj.GetType() == typeof(TGameObjectType))
            .ToArray();

        public IEnumerable<GameObject> GetObjectAt(Vector2D position) => GameObjects.Values.Where(obj => obj.Position == position);
        public int GetCountByObjectName(string objectName) => GameObjects
            .Count(e => e.Value.ObjectTypeName
            .Equals(objectName, System.StringComparison.OrdinalIgnoreCase));

        public void RemoveObjectAt(Vector2D position, GameObject @object)
        {
            GameObject target = GameObjects.First(g => g.Value.Position == position).Value;
            GameObjects.Remove(target.HashCode);
        }

        public bool RemoveObject(GameObject @object)
        {
            GameObjects.Remove(@object.HashCode);
            CurrentTickRemovedObjects.Add(@object);
            OnObjectRemoved?.Invoke(@object);
            return true;
        }

        internal bool UseDefaultLogic { get; set; } =true;
        internal void AddToQueue(ICommand command) => CommandStack.Push(command);
        public bool IsWebSocketRunning => WebSocketServer.IsListening;
        internal void RegisterNewWebSocketClient(string clientHashCode)
        {
            this.LoggingService.LogMessage($"User Connected : ${clientHashCode}");
            Client newClient = new Client(clientHashCode);
                
            if (!Clients.ContainsKey(clientHashCode))
            {
                Clients.Add(clientHashCode, new Client(clientHashCode));
            }
            else throw new ClientAlreadyAddedException();

            OnNewClientRegistred?.Invoke(this, newClient);
        }

        private void StartInternal() => ServerLogic();
        public void Start() => ThreadPool.QueueUserWorkItem(o => ServerLogic());

        public void SetTickInterval(int newTick) => TickInterval = newTick;       
        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => Start());
        public void AddCommandToQueue(ICommand command) => CommandStack.Push(command);
        internal void AddHost(WebSocketServiceHost host) => Host = host;
    }
}
