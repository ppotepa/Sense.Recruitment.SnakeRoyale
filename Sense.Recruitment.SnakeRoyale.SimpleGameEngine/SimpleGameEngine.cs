using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
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
        public readonly Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        private int ticksCount = 0;
      
        public SimpleGameEngine(IGameEngineConfig config, ILoggingService loggingService, IEnumerable<GameLogic> gameLogic)
        {
            LoggingService = loggingService;
            Config = config;
            Behaviours = gameLogic.ToList();
        }
        internal void Tick()
        {
            Behaviours.ForEach(behaviour => behaviour.Apply(this));
            TickCompleted?.Invoke();
            Console.Title = 
                $"Objects:{GameObjects.Values.Count} " +
                $"Snakes:{GameObject.GetCountByName("Snake")} " +
                $"Apples:{GameObject.GetCountByName("Apple")} " +
                $"Ticks:{ticksCount}";

            Thread.Sleep(100);
            ticksCount++;
        }

        internal void MainLogic() 
        {
            while (IsRunning)
            {
                Tick();
            }
        }

        internal void runInternal()
        {
            ThreadPool.QueueUserWorkItem(o => MainLogic());
        }

        public void Run() => new Task(runInternal).Start();
    }
}
