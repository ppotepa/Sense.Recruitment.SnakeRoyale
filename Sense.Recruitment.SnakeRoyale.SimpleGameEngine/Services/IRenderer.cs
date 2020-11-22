namespace Sense.Recruitment.SnakeRoyale.Engine.Services
{
    public interface IRenderer
    {
        void Render();
        void RenderSingleObject(GameObject @object);
        void Initialize();
    }
}
