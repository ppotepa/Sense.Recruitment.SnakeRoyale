using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Threading;
using WebSocket = WebSocketSharp.WebSocket;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network
{
    public class InternalWebSocketClient
    {
        private readonly SimpleGameServer Server;
        private const string webSocketUrl = "ws://127.0.0.1:2137/command";
        private readonly int TickRate = 20;
        private readonly Func<ResolvedCommandType, Command> Factory;

        public InternalWebSocketClient(SimpleGameServer server)
        {
            Server = server;
        }

        public void Start() => ThreadPool.QueueUserWorkItem(o => InternalLogic());
        public void AwaitNewMessage() => ThreadPool.QueueUserWorkItem(o => Await());
        public void Await()
        { 

        }

        internal void InternalLogic()
        {
            using (WebSocket client = new WebSocket(webSocketUrl))
            {
                client.OnMessage += (sender, e) =>
                {
                    Console.WriteLine("Received data from server" + e.Data);
                };

                client.OnOpen += (sender, e) =>
                {
                    //client.Send("Test");
                };
                    
                client.Connect();

                while (Server.IsWebSocketRunning)
                {
                    client.Send("createobject x:20 y:30 predefinedTypeName:apple");
                    Thread.Sleep(200);
                }
            }
        }
    }
}
