using Sense.Recruitment.SnakeRoyale.Engine.Extensions;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
using Sense.Recruitment.SnakeRoyale.Engine.Server.Requests;
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
        public readonly ILoggingService LoggingService;
        public SimpleGameServer(IEnumerable<GameLogicBehaviour> behaviours, WebSocketServer webSocketServer, ILoggingService loggingService)
        {
            Behaviours = behaviours.ToList();
            WebSocketServer = webSocketServer;
            LoggingService = loggingService;
        }

        private int TicksCount = 0;
        public int TickInterval { get; private set; } = 500;
        private List<GameObject> CurrentTickRemovedObjects = new List<GameObject>();

        private void ServerLogic()
        {
            IsRunning = true;
            while (IsRunning)
            {
                ServerTick();
                ProcessCommands();

                string broadCastData = new ServerStateResponse()
                {
                    GameObjects = GameObjects.Values,
                    RemovedObjects = CurrentTickRemovedObjects
                }
                .ToJson();

                Host.Sessions.Broadcast(broadCastData);

                if (CurrentTickRemovedObjects.Any()) 
                {
                    CurrentTickRemovedObjects.Clear();
                }

                TicksCount++;
                Console.Title = $"Objects {GameObjects.Count} Ticks:{TicksCount}";
            }
        }

        //Could actually be a nice Func?
        internal void ServerTick()
        {
            Behaviours.ForEach(b => b.ApplyTo(this));            
            OnTickCompleted?.Invoke();  
            Thread.Sleep(TickInterval);
        }

        internal void ProcessCommands()
        {
            while (CommandStack.Count > 0)
            {
                ICommand command =  CommandStack.Pop();
                LoggingService.LogMessage($"Executing command {command.GetType().Name}.");
                command.Execute();
            }
        }
    }
}
