using Autofac;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Modules;
using System;

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
            SimpleGameEngine engine = container.Resolve<SimpleGameEngine>();

            engine
                .Initialize()
                //.LoadConfiguration()
                //.LoadAssets()
                //.LoadStages()
                //.UseDefaultLogic()
                .Run();

            while (engine.IsRunning)
            {
              //DoSomeExtraLogic
            }
        }
    }
}
