using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Modules
{
    public class CommandResolverModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Type[] allAvailableCommands = Command.GetAvailableCommands();
            Type[] allAvailableParameters = Command.GetAvailableParameters();

            builder.RegisterType<InputStringCommandResolver>()
                .SingleInstance()
                .As<ICommandResolver<string>>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("availableCommands", allAvailableCommands),
                    new NamedParameter("availableParameters", allAvailableParameters),
                })
                .InstancePerLifetimeScope();
        }
    }
}
