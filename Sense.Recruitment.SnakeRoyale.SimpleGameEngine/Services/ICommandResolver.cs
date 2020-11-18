using Sense.Recruitment.SnakeRoyale.Engine.IO;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services
{
    public interface ICommandResolver<in TInputType>
    {
        void InitializeRouter();
        ResolvedCommandType ResolveCommand(TInputType input);
    }
}
