using Autofac;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleLoggingService;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer;
using System;

namespace Sense.Recruitment.SnakeRoyale.Demo.Modules
{
    public class GameEngineModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IGameEngineConfig config = new GameEngineConfig().LoadConfiguration("test");
            
            builder.RegisterType<ConsoleLoggingService>().SingleInstance().As<ILoggingService>();
            builder.RegisterType<ConsoleRenderer>().SingleInstance().As<IRenderer>();
            builder.RegisterType<GameEngineConfig>().SingleInstance().As<IGameEngineConfig>();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(type => type.IsSubclassOf(typeof(GameLogic)))
                  .As<GameLogic>()
                  .InstancePerDependency();

            builder.RegisterType<SimpleGameEngine>()
                .SingleInstance()
                .WithParameters(new[] 
                {
                    new NamedParameter("config", config),
                })
                .AsSelf();
        }
    }
}
