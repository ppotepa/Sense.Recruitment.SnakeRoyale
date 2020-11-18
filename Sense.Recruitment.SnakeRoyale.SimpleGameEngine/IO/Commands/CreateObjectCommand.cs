using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using System;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class CreateObjectCommand : Command, ICommand
    {
        public CreateObjectCommand(SimpleGameEngine engine, CreateObjectCommandParameters parameters)
        {
            Parameters = parameters;
            Engine = engine;
        }

        private readonly CreateObjectCommandParameters Parameters;
        private readonly SimpleGameEngine Engine;

        public override void Execute()
        {
            var @object = GameObject.Create
            (
                objectName: "Test",
                playable: false,
                isSolid: false,
                bitmapName: null,
                position: new Vector2D(x: 25, y: 25),
                velocity: new Vector2D(x: 1, y: 0),
                roration: 0,
                scale: 1,
                objectTypeName: "Snake"
            );
            Engine.AddObject(@object);
        }

        public override Task<string> ExecuteAsync()
        {
            throw new NotImplementedException();
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
