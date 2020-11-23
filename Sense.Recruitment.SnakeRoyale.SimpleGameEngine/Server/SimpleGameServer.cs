using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
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
        private readonly WebSocketServer WebSocketServer;
        public SimpleGameServer(IEnumerable<GameLogicBehaviour> behaviours, WebSocketServer webSocketServer)
        {
            Behaviours = behaviours.OrderBy(b => b.Priority).ToList();
            WebSocketServer = webSocketServer;
        }

        private int TicksCount = 0;

        public void Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                ServerTick();
                while (CommandStack.Count > 0)
                {
                    ICommand command = CommandStack.Pop();
                    command.Execute();
                }
            }
            TicksCount++;
        }

        //Could actually be a nice Func?
        internal void ServerTick()
        {
            Behaviours.ForEach(b => b.ApplyTo(this));
            OnTickCompleted?.Invoke();
            Thread.Sleep(100);
        }

        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => Start());
        public void AddCommandToQueue(ICommand command) => CommandStack.Push(command);
    }
}
