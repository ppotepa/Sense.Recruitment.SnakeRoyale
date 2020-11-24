using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Demo.Handlers;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Modules;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sense.Recruitment.SnakeRoyale.Client
{
    static class Program
    {
        private static IContainer container;
        private static SimpleGameEngine engine;    

        private static Command CommandFactory(ResolvedCommandType resolvedCommand)
        {
            IEnumerable<Parameter> parameters = resolvedCommand
                .Parameters
                .Select(parameter => new NamedParameter(parameter.Name, parameter.Value));

            var movePlayerCommandParameters
                = container.ResolveNamed<CommandParameters>(resolvedCommand.ParametersType.Name, parameters);

            Parameter[] injectParams = new Parameter[]
            {
                    new TypedParameter(typeof(SimpleGameEngine), engine),
                    new TypedParameter(type: movePlayerCommandParameters.GetType(), value:movePlayerCommandParameters),
            };

            return  (Command) container.Resolve(resolvedCommand.CommandType, injectParams);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            ConsoleRenderer renderer;
            SimpleGameServer server;

            ContainerBuilder builder = new ContainerBuilder();
            builder
                .RegisterModule(new CommandModule())                
                .RegisterModule(new GameEngineModule(CommandFactory));

            container = builder.Build();
            engine = container.Resolve<SimpleGameEngine>();

            renderer = (ConsoleRenderer) container.Resolve<IRenderer>();
            renderer.Initialize();
            server = container.Resolve<SimpleGameServer>();

            server.OnNewClientRegistred += ConnectionHandlers.CreatePlayer;

            while (engine.IsRunning)
            {
                Thread.Sleep(100);
                //CommandTest();
            }
        }
    }
}
