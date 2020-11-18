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
        private static readonly Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        public delegate void TickCompleted();
        public event TickCompleted StartRendering;
      
        public SimpleGameEngine(IGameEngineConfig config, ILoggingService loggingService, IEnumerable<GameLogic> gameLogic)
        {
            LoggingService = loggingService;
            Config = config;
            Behaviours = gameLogic.ToList();
        }
        internal void MainThreadFunction() 
        {
            while (IsRunning)
            {
                Behaviours.ForEach(behaviour =>
                {
                    behaviour.Apply(this);
                });
                Thread.Sleep(500);
            }
        }

        internal void runInternal()
        {
            ThreadPool.QueueUserWorkItem(o => MainThreadFunction());
        }

        public void Run() => new Task(runInternal).Start();
    }
}
