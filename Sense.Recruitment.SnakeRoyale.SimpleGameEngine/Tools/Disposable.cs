using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Tools
{
    public abstract class Disposable : IDisposable
    {
        private bool _disposed = false;
        ~Disposable() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected abstract void Dispose(bool disposing);
        
    }
}
