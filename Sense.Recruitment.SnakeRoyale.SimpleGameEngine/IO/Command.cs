using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public abstract partial class Command : ICommand
    {
        public abstract void Execute();
        private static Type[] _getTypes<TTargetType>() => 
                AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())                
                .Where(t => t.IsSubclassOf(typeof(TTargetType)))
                .ToArray();
        public static Type[] GetAvailableCommands() => _getTypes<Command>();
        public static Type[] GetAvailableParameters() => _getTypes<CommandParameters>();
        public abstract Task<string> ExecuteAsync();
    }
    public abstract class CommandParameters { }
}
