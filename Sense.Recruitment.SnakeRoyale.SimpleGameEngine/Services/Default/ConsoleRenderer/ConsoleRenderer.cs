using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly ILoggingService LoggingService;
        private readonly SimpleGameServer Server;
        private bool Initialized = false;

        public ConsoleRenderer(SimpleGameServer server, ILoggingService loggingService)
        {
            LoggingService = loggingService;
            Server = server;
        }
        private int PreviousObjectCount = 0;
        Dictionary<string, char> TypeNamesCharacters = new Dictionary<string, char>();
        private void ResolveTypeNames() => TypeNamesCharacters = Server.GameObjects.Values
                .Select((@object) => @object.ObjectTypeName)
                .Distinct()
                .Select((string objectName, int index) => (objectName, index))
                .ToDictionary(pair => pair.objectName, objectIndex => (char)(objectIndex.index + 65));

        public void Initialize()
        {
            Console.CursorVisible = false;
            Server.OnTickCompleted += Render;
            Initialized = true;
            ResolveTypeNames();
        }

        public void Render()
        {
            if (PreviousObjectCount != Server.GameObjects.Values.Count)
            {
                ResolveTypeNames();
                PreviousObjectCount = Server.GameObjects.Values.Count;
            }

            Console.Clear();
            List<GameObject> allObjects = Server.GameObjects.Values.ToList();

            if (Initialized)
            {
                allObjects.ForEach(RenderSingleObject);
            }
            else
            {
                throw new RendererNotInitializedException($"{nameof(ConsoleRenderer)} was not initialized.");
            }
        }

        public void RenderSingleObject(GameObject @object)
        {
            if (@object.Position.X > 0 && @object.Position.Y > 0)
            {
                try
                {
                    Console.SetCursorPosition(@object.Position.X, @object.Position.Y);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //clean block
                }
                finally
                {
                    if (@object.ObjectTypeName == "Snake") Console.ForegroundColor = ConsoleColor.Blue;
                    else Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(TypeNamesCharacters[@object.ObjectTypeName]);
                }
            }
        }
    }
}
