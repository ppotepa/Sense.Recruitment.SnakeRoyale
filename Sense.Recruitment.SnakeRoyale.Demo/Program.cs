using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Demo.Modules;
using Sense.Recruitment.SnakeRoyale.Engine;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sense.Recruitment.SnakeRoyale.Demo
{
    static class Program
    {
        #region properties
        private static readonly IContainer container = GetContainer();
        private static readonly ICommandResolver<string> stringResolver = container.Resolve<ICommandResolver<string>>();
        private static readonly ILoggingService loggingService = container.Resolve<ILoggingService>();
        private static readonly SimpleGameEngine engine = container.Resolve<SimpleGameEngine>();   
        private static readonly IRenderer renderer = container.Resolve<IRenderer>();
        #endregion properties

        private static IContainer GetContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
           
            builder.RegisterModule(new CommandModule())
                .RegisterModule(new CommandResolverModule())
                .RegisterModule(new GameEngineModule());

            return builder.Build();
        }
        private static void CommandRecieverTest()
        {
            //string userInput = $"createobject x:{new Random().Next(1, 20)} y:{new Random().Next(1, 20)} predefinedTypeName:apple";
            string userInput = $"test message:KochamAdriankaCommand";
            ResolvedCommandType commandResolved = stringResolver.ResolveCommand(userInput);
            IEnumerable<Parameter> parameters = commandResolved
                .Parameters
                .Select(parameter => new NamedParameter(parameter.Name, parameter.Value))
                .ToArray();

            var movePlayerCommandParameters
                = container.ResolveNamed<CommandParameters>(commandResolved.ParametersType.Name, parameters);

            Parameter[] injectParams = new Parameter[]
            {
                    new TypedParameter(typeof(SimpleGameEngine), engine),
                    new TypedParameter(type: movePlayerCommandParameters.GetType(), movePlayerCommandParameters),
            };

            Command command = (Command)container.Resolve(commandResolved.CommandType, injectParams);
            command.AddToQueue();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            renderer.Initialize();
            engine
                .LoadConfiguration()
                .LoadAssets()
                .LoadStages()
                .UseDefaultLogic()
                .Run();

            while (engine.IsRunning)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
