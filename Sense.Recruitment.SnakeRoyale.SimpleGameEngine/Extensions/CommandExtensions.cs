using Newtonsoft.Json;
using Sense.Recruitment.SnakeRoyale.Engine.IO;

namespace Sense.Recruitment.SnakeRoyale.Engine.Extensions
{
    public static class CommandExtensions
    {
        public static string ToJson(this Command @this) => JsonConvert.SerializeObject(@this);
      
    }
}
