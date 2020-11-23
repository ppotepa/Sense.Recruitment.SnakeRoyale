namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public interface ICommand
    {
        void Execute();
        void Publish();
    }
}
