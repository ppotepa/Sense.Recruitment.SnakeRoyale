using Autofac;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Modules;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer;
using System;
using System.Threading;

namespace Sense.Recruitment.SnakeRoyale.Client
{
    static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder
                .RegisterModule(new CommandModule())
                .RegisterModule(new CommandResolverModule())
                .RegisterModule(new GameEngineModule());

            IContainer container = builder.Build();

            SimpleGameServer server = container.Resolve<SimpleGameServer>();
            SimpleGameEngine engine = container.Resolve<SimpleGameEngine>();
            ConsoleRenderer renderer = (ConsoleRenderer) container.Resolve<IRenderer>();

            server.OnTickCompleted += renderer.Render;
            renderer.Initialize();

            engine
                .Initialize()
                //.LoadConfiguration()
                //.LoadAssets()
                //.LoadStages()
                //.UseDefaultLogic()
                .Run();

            while (engine.IsRunning)
            {
                Thread.Sleep(100);
            }
        }
    }
}
