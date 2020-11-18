using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly ILoggingService LoggingService;
        private readonly SimpleGameEngine Engine;
        private bool Initialized = false;

        public ConsoleRenderer(SimpleGameEngine engine, ILoggingService loggingService)
        {
            LoggingService = loggingService;
            Engine = engine;
            
        }

        public void Initialize()
        {
            Engine.TickCompleted += Render;
            Initialized = true;
        }

        private void RenderObject(GameObject @object)
        {
            Console.SetCursorPosition(@object.Position.X, @object.Position.Y);
            switch (@object.ObjectTypeName)
            {
                case "Apple": Console.Write("a");break;
                case "Snake": Console.Write("x");break;
            };
            
        }

        public void Render()
        {
            if (Initialized)
            {
                Console.Clear();
                List<GameObject> allObjects = Engine.GameObjects.Values.ToList();
                allObjects.ForEach(RenderObject);
            }
            else
            {
                throw new RendererNotInitializedException($"{nameof(ConsoleRenderer)} was not initialized.");
            }
        }
    }
}
