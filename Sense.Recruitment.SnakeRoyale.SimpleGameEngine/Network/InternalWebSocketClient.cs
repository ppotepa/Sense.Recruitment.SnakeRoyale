using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Linq;
using System.Threading;
using WebSocket = WebSocketSharp.WebSocket;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network
{
    public class InternalWebSocketClient
    {
        private readonly SimpleGameServer Server;
        private const string webSocketUrl = "ws://127.0.0.1:2137/command";
        private readonly int TickRate = 20;

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
                    //Console.WriteLine("Received data from server" + e.Data);
                };

                client.OnOpen += (sender, e) =>
                {
                    //client.Send("Test");
                };
                    
                client.Connect();

                Random rnd = new Random();
                while (Server.IsWebSocketRunning)
                {
                    //try
                    //{
                    //    //client.Send($"moveplayer hashCode:{Server.GameObjects.Values.First(e => e.Playable is true).HashCode} x:1 y:0");
                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}
                    
                    Thread.Sleep(200);
                }
            }
        }
    }
}
