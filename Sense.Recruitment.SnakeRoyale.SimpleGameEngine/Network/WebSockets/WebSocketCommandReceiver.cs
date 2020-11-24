using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network.WebSocketsBehaviours
{
    //"//ws:command"
    public class WebSocketCommandReceiver : WebSocketBehavior
    {
        public WebSocketCommandReceiver() { }
        private ICommandResolver<string> CommandResolver;
        private Func<ResolvedCommandType, Command> Factory { get; set; }

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"PlayerCommand received command : {e.Data}");
            ResolvedCommandType resolved = CommandResolver.ResolveCommand(e.Data);
            Command command = Factory(resolved);
            command.Publish();
        }

        protected override void OnOpen()
        {
            Console.WriteLine($"PlayerCommand open");
        }

        public WebSocketCommandReceiver UseResolver(ICommandResolver<string> resolver)
        {
            CommandResolver = resolver;
            return this;
        }
  
       public void AddCommandContainer(ICommandResolver<string> resolver) => CommandResolver = resolver;
       public void UseFactory(Func<ResolvedCommandType, Command> factory) => Factory = factory;
    }
}
    