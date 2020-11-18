﻿using System.Threading.Tasks;

namespace Sense.Recruitment.SnakeRoyale.Engine.IO
{
    public interface ICommand
    {
        void Execute();
        Task<string> ExecuteAsync();
    }

}
