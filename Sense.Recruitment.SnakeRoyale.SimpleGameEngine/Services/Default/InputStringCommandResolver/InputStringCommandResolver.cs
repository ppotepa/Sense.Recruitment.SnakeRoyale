using Sense.Recruitment.SnakeRoyale.Engine.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services
{
    public partial class InputStringCommandResolver : ICommandResolver<string>
    {
        private readonly Type[] availableCommands;
        private readonly Type[] availableParameters;
        private Dictionary<string, (Type CommandType, Type ParametersType, ParameterInfo[] ConstructorParams)> router ;

        public InputStringCommandResolver(Type[] availableCommands, Type[] availableParameters)
        {
            this.availableCommands = availableCommands;
            this.availableParameters = availableParameters;
        }

        public Dictionary<string, (Type CommandType, Type ParametersType, ParameterInfo[] ConstructorParams)> Router 
        {
            get 
            {
                if (router == null)
                {
                    InitializeRouter();
                }
                return router;
            }
            set => router = value;
        }

        public void InitializeRouter()
        {
            Router =
                availableCommands
                .ToDictionary(c => c.Name.Replace("Command", string.Empty).ToLower(), c =>
                {
                    var Command = availableCommands.First(av => av.Name.StartsWith(c.Name.Replace("Command", "")));  
                    var Params = availableParameters.First(av => av.Name.StartsWith(c.Name.Replace("Parameters", "")));
                    var ConstructorParams = availableParameters.GetType().GetConstructors().First().GetParameters();
                    return (Command, Params, ConstructorParams);
                });
        }

        public ResolvedCommandType ResolveCommand(string input)
        {
            string[] commandInputSplit = input.Replace(DoubleSpace, string.Empty)
                .Trim(WhiteSpace)
                .Split(WhiteSpace);

            string commandName = commandInputSplit[0];

            Type commandType = Router[commandName].CommandType;
            Type parametersType = Router[commandName].ParametersType;
            ParameterInfo[] constructorParams = Router[commandName].ConstructorParams;

            IEnumerable<(string Name, object Value)> parameters = commandInputSplit
            .Skip(1)
            .Take(commandInputSplit.Length - 1)
            .Select((param, index) =>
            {
                object valueConverted = default;
                string[] paramSplit = param.Split(Separator);
                try
                {
                    valueConverted = Convert.ChangeType(paramSplit[1], constructorParams[0].ParameterType);
                }
                catch (NotSupportedException)
                {
                    var exceptionParameters = new string[]
                    {
                        paramSplit[0],
                        paramSplit[1],
                        constructorParams[index].ParameterType.Name
                    };
                    var message = string.Format(NotFoundExceptionMessage, exceptionParameters);
                    throw new NotSupportedException(message);
                }
                return
                (
                    Name: paramSplit[0],
                    Value: valueConverted
                );
            });

            return new ResolvedCommandType
            {
                CommandType = commandType,
                ParametersType = parametersType,
                Parameters = parameters,
            };
        }
    }
}
