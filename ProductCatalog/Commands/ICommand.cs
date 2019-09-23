using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public interface ICommand
    {
        bool IsSuccesful { get; set; }
        void Execute();
    }
}
