using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Network;
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
        private readonly Func<ResolvedCommandType, Command> CommandFactory;
        public GameEngineModule(Func<ResolvedCommandType, Command> commandFactory) 
        {
            CommandFactory = commandFactory;
        }

        protected override void Load(ContainerBuilder builder)
        {
            IGameEngineConfig config = new GameEngineConfig().LoadConfiguration("test");
            WebSocketServiceHost host = null; 

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

            builder
               .RegisterType<InternalWebSocketClient>()
               .SingleInstance()
               .AsSelf();

            builder.RegisterType<WebSocketServer>()
                .SingleInstance()
                .As<WebSocketServer>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("address", IPAddress.Parse("127.0.0.1")),
                    new NamedParameter("port", 2137),
                })
                .OnActivated(args =>
                {
                    Type[] allAvailableCommands = Command.GetAvailableCommands();
                    Type[] allAvailableParameters = Command.GetAvailableParameters();

                    WebSocketServer instance = args.Instance;
                    instance.AddWebSocketService<WebSocketCommandReceiver>("/command", (c) => 
                    {
                        c.Initialize(new InputStringCommandResolver(allAvailableCommands, allAvailableParameters), CommandFactory);                        
                    });
                    instance.Start();
                    instance.WebSocketServices.TryGetServiceHost("/command", out host);
                });

            builder
                .RegisterType<SimpleGameServer>()
                .SingleInstance()
                .As<SimpleGameServer>()
                .OnActivated(a => a.Instance.AddHost(host));
                

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
            .AsSelf()
            .OnActivated(engineBuilder => engineBuilder.Instance.Run());
        }
    }
}
