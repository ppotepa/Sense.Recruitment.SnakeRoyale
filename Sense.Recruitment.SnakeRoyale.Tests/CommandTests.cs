using Autofac.Core;
using Autofac.Extras.Moq;
using NUnit.Framework;
using Sense.Recruitment.SnakeRoyale.Engine.Commands;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;
using System.Collections.Generic;
using WebSocketSharp.Server;

namespace Sense.Recruitment.SnakeRoyale.Tests
{

    public class CommandTests
    {
        [Test]
        public void CreateObjectTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                Moq.Mock<ILoggingService> console = mock.Mock<ILoggingService>();
                Moq.Mock<WebSocketServer> webSocketServer = mock.Mock<WebSocketServer>();

                var serverMock = mock.Mock<SimpleGameServer>();
                serverMock.Setup(s => s.Start());
            }
        }

        private object IEnumerable<T>()
        {
            throw new NotImplementedException();
        }
    }
}