using System;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public class ResolvedCommandType
    {
        public ResolvedCommandType() { }
        public Type CommandType { get; set; }
        public Type ParametersType { get; set; }
        public IEnumerable<(string Name, object Value)> Parameters { get; set; }
    }

}
