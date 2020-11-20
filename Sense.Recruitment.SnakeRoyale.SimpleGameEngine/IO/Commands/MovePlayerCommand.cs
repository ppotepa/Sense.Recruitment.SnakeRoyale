using Sense.Recruitment.SnakeRoyale.Engine.IO;
using System;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class MovePlayerCommand : Command, ICommand
    {
        public MovePlayerCommand(SimpleGameEngine engine, MovePlayerCommandParameters parameters) : base(engine)
        {
            Engine = engine;
            Parameters = parameters;
        }

        public readonly SimpleGameEngine Engine;
        public readonly MovePlayerCommandParameters Parameters;

        public override void Execute()
        {
            //Thread.Sleep(5);
        }

        public override async Task<string> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class MovePlayerCommandParameters : CommandParameters
    {
        public readonly int x, y;
        public MovePlayerCommandParameters(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
