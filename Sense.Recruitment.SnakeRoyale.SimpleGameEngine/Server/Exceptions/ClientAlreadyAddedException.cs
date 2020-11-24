using System;
using System.Runtime.Serialization;

namespace Sense.Recruitment.SnakeRoyale.Engine.Server
{
    [Serializable]
    internal class ClientAlreadyAddedException : Exception
    {
        public ClientAlreadyAddedException()
        {
        }

        public ClientAlreadyAddedException(string message) : base(message)
        {
        }

        public ClientAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClientAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}