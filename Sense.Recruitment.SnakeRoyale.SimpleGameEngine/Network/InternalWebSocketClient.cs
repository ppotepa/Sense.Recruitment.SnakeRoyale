using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System.Threading;
using WebSocket = WebSocketSharp.WebSocket;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network
{
    public class InternalWebSocketClient
    {
        private readonly SimpleGameServer Server;
        private const string webSocketUrl = "ws://127.0.0.1:2137/command";
        public InternalWebSocketClient(SimpleGameServer server)
        {
            Server = server;
        }

        public void Start() => ThreadPool.QueueUserWorkItem(o => InternalLogic());
        internal void InternalLogic()
        {
            using (WebSocket client = new WebSocket(webSocketUrl))
            {
                client.OnMessage += (sender, e) =>
                {
                    
                };

                client.OnOpen += (sender, e) =>
                {
                    //some handshake logic
                };
                    
                client.Connect();
             
                while (Server.IsWebSocketRunning)
                {
                    Thread.Sleep(200);
                }
            }
        }
    }
}
