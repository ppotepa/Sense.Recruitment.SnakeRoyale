using Newtonsoft.Json;

namespace Sense.Recruitment.SnakeRoyale.Engine.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object @object) => JsonConvert.SerializeObject(@object);
    }
}
