using System;
using System.Runtime.Serialization;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer
{
    [Serializable]
    internal class RendererNotInitializedException : Exception
    {
        public RendererNotInitializedException()
        {
        }

        public RendererNotInitializedException(string message) : base(message)
        {
        }

        public RendererNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RendererNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}