using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public abstract partial class Command : ICommand
    {
        protected readonly SimpleGameServer Server;
        protected Command(SimpleGameServer server) => Server = server;
        public abstract void Execute();
        private static Type[] GetTypes<TTargetType>() =>
                AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly =>
                {
                    return assembly
                    .GetTypes()
                    .Where(type => type.IsSubclassOf(typeof(TTargetType)));
                })
            .ToArray();

        public static Type[] GetAvailableCommands() => GetTypes<Command>();
        public static Type[] GetAvailableParameters() => GetTypes<CommandParameters>();
        public void Publish() => Server.AddToQueue(this);
    }
    public abstract class CommandParameters { }
}
