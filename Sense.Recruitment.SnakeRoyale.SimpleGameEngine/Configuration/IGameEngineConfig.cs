using System.IO;

namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public interface IGameEngineConfig
    {
        GameEngineConfig LoadConfiguration(string configFilePath);
        object GetConfigKey(string key);
    }
}