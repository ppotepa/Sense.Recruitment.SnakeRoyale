using Autofac;
using Autofac.Core;
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
        private static ICommandResolver<string> resolver;
        private static SimpleGameEngine engine;
        private static SimpleGameServer server;
        private static ConsoleRenderer renderer;

        private static void CommandTest()
        {
            var userInput = "createobject x:20 y:20 predefinedTypeName:snake";
            ResolvedCommandType commandResolved = resolver.ResolveCommand(userInput);
            IEnumerable<Parameter> parameters = commandResolved
                .Parameters
                .Select(parameter => new NamedParameter(parameter.Name, parameter.Value));

            var movePlayerCommandParameters
                = container.ResolveNamed<CommandParameters>(commandResolved.ParametersType.Name, parameters);

            Parameter[] injectParams = new Parameter[]
            {
                    new TypedParameter(typeof(SimpleGameEngine), engine),
                    new TypedParameter(type: movePlayerCommandParameters.GetType(), value:movePlayerCommandParameters),
            };

            Command command = (Command)container.Resolve(commandResolved.CommandType, injectParams);
            command.Publish();
            Thread.Sleep(100);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            //to be moved to engine
            ContainerBuilder builder = new ContainerBuilder();

            builder
                .RegisterModule(new CommandModule())
                .RegisterModule(new CommandResolverModule())
                .RegisterModule(new GameEngineModule());

            container = builder.Build();

            server = container.Resolve<SimpleGameServer>();
            engine = container.Resolve<SimpleGameEngine>();
            renderer = (ConsoleRenderer) container.Resolve<IRenderer>();
            resolver = container.Resolve<ICommandResolver<string>>();

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
                CommandTest();
            }
        }
    }
}
