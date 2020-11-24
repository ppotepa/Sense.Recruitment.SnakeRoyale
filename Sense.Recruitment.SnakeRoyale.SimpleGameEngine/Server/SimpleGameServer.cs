using Newtonsoft.Json;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    public partial class SimpleGameServer
    {
        public bool IsRunning { get; private set; }
        private readonly Stack<ICommand> CommandStack = new Stack<ICommand>();
        public readonly Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        private readonly List<GameLogicBehaviour> Behaviours;
        private readonly Dictionary<string, Client> Clients = new Dictionary<string, Client>();

        private readonly WebSocketServer WebSocketServer;
        private WebSocketServiceHost Host;
        private readonly ILoggingService LoggingService;
        public SimpleGameServer(IEnumerable<GameLogicBehaviour> behaviours, WebSocketServer webSocketServer, ILoggingService loggingService)
        {
            Behaviours = behaviours.OrderBy(b => b.Priority).ToList();
            WebSocketServer = webSocketServer;
            LoggingService = loggingService;
        }

        private int TicksCount = 0;
        private void StartInternal() => ServerLogic();
        public void Start() => ThreadPool.QueueUserWorkItem(o => ServerLogic());
        private void ServerLogic()
        {
            IsRunning = true;
            while (IsRunning)
            {
                ServerTick();
                ProcessCommands();

                var broadCastData = GetBroadcatData();
                Host.Sessions.Broadcast(broadCastData);

                TicksCount++;
                Console.Title = $"Objects {GameObjects.Count} Ticks:{TicksCount}";
            }
        }

        //Could actually be a nice Func?
        internal void ServerTick()
        {
            Behaviours.ForEach(b => b.ApplyTo(this));            
            OnTickCompleted?.Invoke();  
            Thread.Sleep(100);
        }

        internal void ProcessCommands()
        {
            while (CommandStack.Count > 0)
            {
                //LoggingService.LogMessage($"[Server] Processing Commands {CommandStack.Count}");
                ICommand command = CommandStack.Pop();
                command.Execute();
            }
        }

        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => Start());
        public void AddCommandToQueue(ICommand command) => CommandStack.Push(command);

        internal void AddHost(WebSocketServiceHost host) => Host = host;
       
    }
}
