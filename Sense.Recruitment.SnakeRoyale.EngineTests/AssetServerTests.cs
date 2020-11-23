using NUnit.Framework;
using Sense.Recruitment.SnakeRoyale.Engine;

namespace Sense.Recruitment.SnakeRoyale.EngineTests
{
    public class AssetsTest
    {
        SimpleGameEngine SimpleGameEngine = new SimpleGameEngine();

        [SetUp]
        public void Setup()
        {
            SimpleGameEngine.Run();
        }

        [Test]
        public void TestAssetLoader()
        {
            Assert.Pass();
        }
    }
}