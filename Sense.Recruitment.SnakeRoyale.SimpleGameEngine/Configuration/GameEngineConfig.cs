namespace Sense.Recruitment.SnakeRoyale.Engine
{
    public sealed class GameEngineConfig : IGameEngineConfig
    {
        public ObjectType[] ObjectTypes { get; set; }
        public class ObjectType
        {
            public string ObjectTypeName { get; set; }
            public string Character { get; set; }
        }
        public object GetConfigKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public GameEngineConfig LoadConfiguration(string configFilePath)
        {
            return default;
        }
    }
}