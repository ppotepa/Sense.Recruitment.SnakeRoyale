using Autofac;
using System.ComponentModel;

namespace Sense.Recruitment.SnakeRoyale.Engine.Extensions
{

    public static class AutoFacExtensions
    {
        public static TOutputObjectType BuildOut<TOutputObjectType>(this ContainerBuilder @this, out TOutputObjectType output) => output = (TOutputObjectType) @this.Build();
       
    };
}
