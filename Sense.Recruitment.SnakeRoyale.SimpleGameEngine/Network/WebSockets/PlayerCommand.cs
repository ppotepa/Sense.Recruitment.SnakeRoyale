using WebSocketSharp;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network.WebSocketsBehaviours
{
    public class PlayerCommand : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
        }
    }
}
    