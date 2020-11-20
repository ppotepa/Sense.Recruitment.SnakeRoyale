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
            Console.CursorVisible = false;
            Engine.TickCompleted += Render;
            Initialized = true;
        }

        private void RenderObject(GameObject @object)
        {
            char charToRender = ' ';
            switch (@object.ObjectTypeName)
            {
                case "Apple": charToRender = 'a'; break;
                case "Snake": charToRender = 'x'; break;
            }
            Console.SetCursorPosition(@object.Position.X, @object.Position.Y);
            Console.Write(charToRender);
        }

        public void Render()
        {
            Console.WriteLine();
            lock (Engine.GameObjects)
            {
                List<GameObject> allObjects = Engine.GameObjects.Values.ToList();
                if (Initialized)
                {
                    allObjects.ForEach(RenderObject);
                }
                else
                {
                    throw new RendererNotInitializedException($"{nameof(ConsoleRenderer)} was not initialized.");
                }
            }
        }
    }
}
