using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public interface ICommand
    {
        void Execute();
        void AddToQueue();
        Task<string> ExecuteAsync();
    }

}
