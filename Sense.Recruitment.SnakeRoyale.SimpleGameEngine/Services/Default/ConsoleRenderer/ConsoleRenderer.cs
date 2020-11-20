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
        private int PreviousObjectCount = 0;
        Dictionary<string, char> TypeNamesCharacters = new Dictionary<string, char>();
        private void ResolveTypeNames() => TypeNamesCharacters = Engine.GameObjects.Values
                .Select(e => e.ObjectTypeName)
                .Distinct()
                .Select((objectName, index) => new { objectName, index })
                .ToDictionary(pair => pair.objectName, objectIndex => (char)(objectIndex.index + 65));
       
        public void Initialize()
        {
            Console.CursorVisible = false;
            Engine.OnTickCompleted += Render;
            Initialized = true;
            ResolveTypeNames();
        }

        private void RenderObject(GameObject @object)
        {
            if (@object.Position.X > 0 && @object.Position.Y > 0)
            {
                try
                {
                    Console.SetCursorPosition(@object.Position.X, @object.Position.Y);
                }
                catch (Exception)
                {

                }
                finally 
                {
                    if (@object.ObjectTypeName == "Snake") Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write(TypeNamesCharacters[@object.ObjectTypeName]);
                }
            }
        }

        public void Render()
        {
            if (PreviousObjectCount != Engine.GameObjects.Values.Count)
            {
                ResolveTypeNames();
                PreviousObjectCount = Engine.GameObjects.Values.Count;
            }

            Console.Clear();
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
