using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    public partial class SimpleGameServer
    {
        public bool IsRunning { get; private set; }
        private readonly Stack<ICommand> CommandQueue = new Stack<ICommand>();
        private readonly Stopwatch StopWatch = new Stopwatch();

        public readonly Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();

        private readonly List<GameLogicBehaviour> Behaviours;
        public SimpleGameServer(IEnumerable<GameLogicBehaviour> behaviours) => Behaviours = behaviours.OrderBy(b => b.Priority).ToList();

        public void Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                ServerTick();
                OnTickCompleted?.Invoke();
            }
        }
        //Could actually be a nice Func?
        internal void ServerTick() => Behaviours.ForEach(b => b.ApplyTo(this));
        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => Start());
        public void AddCommandToQueue(ICommand command) => CommandQueue.Push(command);
    }
}
