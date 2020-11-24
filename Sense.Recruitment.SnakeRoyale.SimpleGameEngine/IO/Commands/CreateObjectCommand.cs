using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class CreateObjectCommand : Command, ICommand
    {
        public CreateObjectCommand(SimpleGameServer server, CreateObjectCommandParameters parameters) : base(server)
        {
            Parameters = parameters;
        }

        private readonly CreateObjectCommandParameters Parameters;

        public override void Execute()
        {
            var @object = GameObject.Create
            (
                objectName: "Test",
                playable: false,
                isSolid: false,
                bitmapName: null,
                position: new Vector2D(x: Parameters.X, y: Parameters.Y),
                velocity: new Vector2D(x: 0, y: 0),
                roration: 0,
                scale: 1,
                objectTypeName: Parameters.PredefinedTypeName, 
                owner:null
            );
           
            Server.AddObject(@object);
        }
    }
    public class CreateObjectCommandParameters : CommandParameters
    {
        public readonly string PredefinedTypeName;
        public readonly int X;
        public readonly int Y;

        public CreateObjectCommandParameters(string predefinedTypeName, int x, int y)
        {
            this.PredefinedTypeName = predefinedTypeName ?? "ERROR";
            this.X = x;
            this.Y = y;
        }
    }
}
