using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class MovePlayerCommand : Command, ICommand
    {
        public MovePlayerCommand(SimpleGameServer server, MovePlayerCommandParameters parameters) : base(server)
        {
            Parameters = parameters;
        }
     
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
        public MovePlayerCommandParameters(string playerId, int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
