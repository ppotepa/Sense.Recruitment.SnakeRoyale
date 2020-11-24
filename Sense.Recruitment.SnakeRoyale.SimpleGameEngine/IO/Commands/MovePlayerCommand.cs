﻿using Sense.Recruitment.SnakeRoyale.Engine.Commands.Exceptions;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Primitives;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using System.Linq;

namespace Sense.Recruitment.SnakeRoyale.Engine.Commands
{
    public class MovePlayerCommand : Command, ICommand
    {
        public MovePlayerCommand(SimpleGameServer server, MovePlayerCommandParameters parameters) : base(server)
        {
            Parameters = parameters;
        }
     
        public readonly MovePlayerCommandParameters Parameters;

        public override void Execute()
        {
            try
            {
                GameObject player = Server.GameObjects.Values
                    .First(@object => @object.Owner != null && @object.Owner.ClientHashCode == Parameters.hashCode);

                player.Velocity = new Vector2D(Parameters.x, Parameters.y);
            }
            catch (UnableToFindPlayerException ex)
            {
                Server.LoggingService.LogMessage(ex.Message);
            }
            finally 
            {
                
            }
        }
    }
    public class MovePlayerCommandParameters : CommandParameters
    {
        public readonly int x, y;
        public readonly string hashCode;

        public MovePlayerCommandParameters(string hashCode, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.hashCode = hashCode;
        }
    }
}
