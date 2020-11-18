namespace Sense.Recruitment.SnakeRoyale.Engine.Objects
{
    public class GameEngineConfig
    {
        public Objecttype[] ObjectTypes { get; set; }
    }

    public class Objecttype
    {
        public string ObjectTypeName { get; set; }
        public string Character { get; set; }
    }
}
