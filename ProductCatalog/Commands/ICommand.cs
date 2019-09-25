using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public interface ICommand : IFailable
    {
        void Execute();
    }
}
