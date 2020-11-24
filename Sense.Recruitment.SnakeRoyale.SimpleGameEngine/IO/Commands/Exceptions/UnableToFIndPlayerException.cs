using System;
using System.Runtime.Serialization;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands.Exceptions
{
    [Serializable]
    internal class UnableToFindPlayerException : Exception
    {
        public UnableToFindPlayerException()
        {
        }

        public UnableToFindPlayerException(string message) : base(message)
        {
        }

        public UnableToFindPlayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnableToFindPlayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}