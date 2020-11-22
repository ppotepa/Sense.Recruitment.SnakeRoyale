using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public abstract partial class Command : ICommand
    {
        protected readonly SimpleGameServer Server;
        protected Command(SimpleGameServer server)
        {
            Server = server;   
        }
        public abstract void Execute();
     
        private static Type[] GetTypes<TTargetType>() => 
                AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())                
                .Where(t => t.IsSubclassOf(typeof(TTargetType)))
                .ToArray();

        public static Type[] GetAvailableCommands() => GetTypes<Command>();
        public static Type[] GetAvailableParameters() => GetTypes<CommandParameters>();
        public abstract Task<string> ExecuteAsync();
        public void Publish() => Server.AddToQueue((ICommand)this);
    }
    public abstract class CommandParameters { }
}
