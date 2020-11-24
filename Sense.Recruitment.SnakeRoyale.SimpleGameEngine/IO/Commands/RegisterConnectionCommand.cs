using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class RegisterConnectionCommand : Command, ICommand
    {
        public RegisterConnectionCommand(SimpleGameServer server, RegisterConnectionCommandParameters parameters) : base(server)
        {
            Parameters = parameters;
        }

        private readonly RegisterConnectionCommandParameters Parameters;

        public override void Execute()
        {
            Server.RegisterNewWebSocketClient(Parameters.UserHash);
        }
    }
    public class RegisterConnectionCommandParameters : CommandParameters
    {
        public readonly string UserHash; 
        public RegisterConnectionCommandParameters(string userHashCode)
        {
            UserHash = userHashCode;
        }
    }
}
