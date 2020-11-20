using Sense.Recruitment.SnakeRoyale.Engine.IO;
using System;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class WriteDupaCommand : Command, ICommand
    {
        public WriteDupaCommand(SimpleGameEngine engine, WriteDupaCommandParameters parameters) : base(engine)
        {
            Parameters = parameters;
            Engine = engine;
        }

        private readonly WriteDupaCommandParameters Parameters;
        private readonly SimpleGameEngine Engine;

        public override void Execute()
        {
            Console.WriteLine(Parameters.Message);
        }

        public override Task<string> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class WriteDupaCommandParameters : CommandParameters
    {
        public readonly string Message;
        public WriteDupaCommandParameters(string message)
        {
            this.Message = message ?? "ERROR";
        }
    }
}
