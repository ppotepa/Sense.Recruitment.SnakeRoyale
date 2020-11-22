using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public partial class SimpleGameEngine
    {
        private int ticksCount = 0;
        private bool IsUsingDefaultLogic { get; set; } = false;
        private bool IsEngineInitialized { get; set; } = false;
        public bool IsRunning { get; private set; } = true;

        private readonly IGameEngineConfig Config;
        private readonly ILoggingService LoggingService;
        private readonly SimpleGameServer Server;
      
        private readonly Stopwatch StopWatch = new Stopwatch();        
       
        public SimpleGameEngine(IGameEngineConfig config, ILoggingService loggingService, SimpleGameServer server)
        {
            LoggingService = loggingService;
            Config = config;
            Server = server;
        }        

        internal void MainLogic()
        {
            Server.Start();
            while (Server.IsRunning)
            {
                Thread.Sleep(100);
                //Some extra logic in engine scope
            }
        }

        internal void RunInternal() => ThreadPool.QueueUserWorkItem(o => MainLogic());
        public void Run() => new Task(RunInternal).Start();

        public SimpleGameEngine Initialize()
        {
            //throw new NotImplementedException();
            return this;
        }
    }
}
