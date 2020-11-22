﻿using Autofac;
using Sense.Recruitment.SnakeRoyale.Engine.Logic;
using Sense.Recruitment.SnakeRoyale.Engine.Server;
using Sense.Recruitment.SnakeRoyale.Engine.Services;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleLoggingService;
using Sense.Recruitment.SnakeRoyale.Engine.Services.Default.ConsoleRenderer;
using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Modules
{
    public class GameEngineModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IGameEngineConfig config = new GameEngineConfig().LoadConfiguration("test");
            
            builder.RegisterType<ConsoleLoggingService>().SingleInstance().As<ILoggingService>();
            builder.RegisterType<ConsoleRenderer>().SingleInstance().As<IRenderer>();
            builder.RegisterType<GameEngineConfig>().SingleInstance().As<IGameEngineConfig>();
            builder.RegisterType<ConsoleLoggingService>().InstancePerDependency().As<ILoggingService>();
            builder.RegisterType<SimpleGameServer>().InstancePerDependency().As<SimpleGameServer>();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(type => type.IsSubclassOf(typeof(GameLogicBehaviour)))
                  .As<GameLogicBehaviour>()
                  .InstancePerDependency();

            builder.RegisterType<SimpleGameEngine>()
            .SingleInstance()
            .WithParameters(new[] 
            {
                new NamedParameter("config", config),
            })
            .AsSelf();
        }
    }
}