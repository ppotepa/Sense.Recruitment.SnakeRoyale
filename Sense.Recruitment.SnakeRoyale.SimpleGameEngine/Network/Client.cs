using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Network
{
    public class Client
    {
        public DateTime LastActivity { get; private set; } = DateTime.Now;
        public Client(string clientHashCode)
        {
            ClientHashCode = clientHashCode;
        }

        public string ClientHashCode { get; }
        public void KeepAlive() => LastActivity = DateTime.Now;
    }
}
