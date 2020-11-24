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

        //how else can i do it ;o
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
            ContainerBuilder builder = new ContainerBuilder();
            builder
                .RegisterModule(new CommandModule())
                //.RegisterModule(new CommandResolverModule())
                //
                .RegisterModule(new GameEngineModule(CommandFactory));

            container = builder.Build();
            engine = container.Resolve<SimpleGameEngine>();
            renderer = (ConsoleRenderer) container.Resolve<IRenderer>();
            renderer.Initialize();
            
            while (engine.IsRunning)
            {
                Thread.Sleep(100);
                //CommandTest();
            }
        }
    }
}
