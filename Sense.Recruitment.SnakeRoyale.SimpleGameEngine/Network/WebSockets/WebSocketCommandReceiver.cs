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
            ResolvedCommandType resolved = CommandResolver.ResolveCommand(e.Data);
            Command command = Factory(resolved);
            command.Publish();
        }

        protected override void OnOpen()
        {
          
        }        

        //theres probably a better way of injecting it
        public void Initialize(ICommandResolver<string> resolver, Func<ResolvedCommandType, Command> factory)
        {
            CommandResolver = resolver;
            Factory = factory;
        }
    }
}
    