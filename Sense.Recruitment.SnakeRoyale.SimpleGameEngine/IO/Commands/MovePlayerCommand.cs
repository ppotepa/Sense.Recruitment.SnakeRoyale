using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
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
            GameObject player = Server.GetObjectByTypeHashCode(Parameters.hashCode);
            player.Velocity = new Vector2D(Parameters.x, Parameters.y);
        }
    }
    public class MovePlayerCommandParameters : CommandParameters
    {
        public readonly int x, y;
        public readonly string hashCode;

        public MovePlayerCommandParameters(string hashCode, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.hashCode = hashCode;
        }
    }
}
