using NUnit.Framework;
using Sense.Recruitment.SnakeRoyale.Engine;

namespace Sense.Recruitment.SnakeRoyale.EngineTests
{
    public class CommandTests
    {
        private static SimpleGameEngine Mock = new SimpleGameEngine();

        //[SetUp]
        //public void Setup()
        //{
        //    Mock.Run();
        //}

        //[Test]
        //private void CommandTest()
        //{
        //    var userInput = "createobject x:20 y:20 predefinedTypeName:snake";
        //    ResolvedCommandType commandResolved = resolver.ResolveCommand(userInput);
        //    IEnumerable<Parameter> parameters = commandResolved
        //        .Parameters
        //        .Select(parameter => new NamedParameter(parameter.Name, parameter.Value));

        //    var movePlayerCommandParameters
        //        = container.ResolveNamed<CommandParameters>(commandResolved.ParametersType.Name, parameters);

        //    Parameter[] injectParams = new Parameter[]
        //    {
        //            new TypedParameter(typeof(SimpleGameEngine), engine),
        //            new TypedParameter(type: movePlayerCommandParameters.GetType(), movePlayerCommandParameters),
        //    };

        //    Command command = (Command)container.Resolve(commandResolved.CommandType, injectParams);
        //    command.Publish();
        //    Thread.Sleep(100);
        //}
    }
}