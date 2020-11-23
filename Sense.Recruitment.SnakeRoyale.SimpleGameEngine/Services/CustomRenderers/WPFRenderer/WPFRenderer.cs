using System.Windows;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.CustomRenderers
{
    public class WPFRenderer : IRenderer
    {
        private readonly Window GameWindow;
        public void Initialize()
        {
            GameWindow.Title = "Snakes WPF Demo";
            GameWindow.Width = 1000;
            GameWindow.Height = 1000;
            //GameWindow.Show();
        }

        public void Render()
        {
            throw new System.NotImplementedException();
        }

        public void RenderSingleObject(GameObject @object)
        {
            throw new System.NotImplementedException();
        }
    }
}
