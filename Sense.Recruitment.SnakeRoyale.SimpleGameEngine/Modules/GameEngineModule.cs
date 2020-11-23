using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Network.WebSocketsBehaviours;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleLoggingService;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer;
using System;
using System.Net;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Modules
{
    public class GameEngineModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IGameEngineConfig config = new GameEngineConfig().LoadConfiguration("test");
            
            builder.RegisterType<ConsoleLoggingService>()
                .InstancePerLifetimeScope()
                .As<ILoggingService>();

            builder.RegisterType<ConsoleRenderer>()
                .SingleInstance()
                .As<IRenderer>();

            builder
                .RegisterType<GameEngineConfig>()
                .SingleInstance()
                .As<IGameEngineConfig>();

            builder.RegisterType<WebSocketServer>()
                .SingleInstance()
                .As<WebSocketServer>()
                .WithParameters(new Parameter[] 
                { 
                    new NamedParameter("ipaddress", IPAddress.Parse("127.0.0.1")),
                    new NamedParameter("port", 2137)
                })
                .OnActivating(args =>
                {
                    WebSocketServer instance = args.Instance;
                    instance.AddWebSocketService<PlayerCommand>(@"/command");
                    instance.Start();
                }); 

            builder.RegisterType<SimpleGameServer>().SingleInstance().As<SimpleGameServer>();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(type => type.IsSubclassOf(typeof(GameLogicBehaviour)))
                  .As<GameLogicBehaviour>()
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
