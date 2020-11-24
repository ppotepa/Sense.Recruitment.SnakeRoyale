using System;

namespace Sense.Recruitment.SnakeRoyale.Engine.Logic
{
    public class LogicPriorityAttribute : Attribute
    {
        public readonly int Priority;

        public LogicPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}