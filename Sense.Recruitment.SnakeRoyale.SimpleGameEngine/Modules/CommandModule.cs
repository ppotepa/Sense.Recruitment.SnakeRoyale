using Autofac;
using Sense.Recruitment.SnakeRoyale.Engine.IO;
using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(type => type.IsSubclassOf(typeof(Command)))
                   .Named<Command>(type => type.Name)
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(type => type.IsSubclassOf(typeof(CommandParameters)))
                  .Named<CommandParameters>(type => type.Name)
                  .AsSelf()
                  .InstancePerDependency();
        }
    }
}
