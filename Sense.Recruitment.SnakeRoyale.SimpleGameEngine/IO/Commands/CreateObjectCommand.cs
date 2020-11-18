using Sense.Recruitment.SnakeRoyale.Engine.IO;
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
            
        }

        public override Task<string> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class CreateObjectCommandParameters : CommandParameters
    {
        public readonly int x, y;
        public CreateObjectCommandParameters(GameObject gameObject)
        {
            GameObject = gameObject;
        }
        public readonly GameObject GameObject;
    }
}
