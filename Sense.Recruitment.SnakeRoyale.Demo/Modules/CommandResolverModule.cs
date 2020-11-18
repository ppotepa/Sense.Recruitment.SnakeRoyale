using Autofac;
using Autofac.Core;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using Sense.Recruitment.SnakeRoyale.Engine.Services;

namespace Sense.Recruitment.SnakeRoyale.Demo.Modules
{
    public class CommandResolverModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var allAvailableCommands = Command.GetAvailableCommands();
            var allAvailableParameters = Command.GetAvailableParameters();

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
