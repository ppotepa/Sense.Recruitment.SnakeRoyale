using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        public bool IsRunning { get; set; } = true;
        private readonly IGameEngineConfig Config;
        private readonly ILoggingService LoggingService;
        private readonly List<GameLogic> Behaviours;
        private readonly Stopwatch stopWatch = new Stopwatch();

        public readonly Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        private int ticksCount = 0;
        private bool usingDefaultLogic = false;
        private List<ICommand> CommandQueue = new List<ICommand>();

        public SimpleGameEngine(IGameEngineConfig config, ILoggingService loggingService, IEnumerable<GameLogic> gameLogic)
        {
            LoggingService = loggingService;
            Config = config;
            Behaviours = gameLogic.OrderBy(behaviour => behaviour.Priority).ToList();
        }

        internal void Tick()
        {
            stopWatch.Start();
                Behaviours.ForEach(behaviour => behaviour.ApplyTo(this));
                OnTickCompleted?.Invoke();
                Thread.Sleep(20);
            stopWatch.Stop();

            Console.Title =
               $"Objects:{GameObjects.Values.Count} " +
               $"Snakes:{GetCountByObjectName("Snake")} " +
               $"Apples:{GetCountByObjectName("Apple")} " +
               $"TickTime:{stopWatch.ElapsedMilliseconds}ms " + 
               $"Ticks:{ticksCount}";
            stopWatch.Reset();
            ticksCount++;
        }

        internal void MainLogic() 
        {
            while (IsRunning)
            {
                Tick();
                new List<ICommand>(CommandQueue).ForEach(c => c.Execute());
            }
        }

        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => MainLogic());
        public void Run() => new Task(RunInternal).Start();
    }
}
