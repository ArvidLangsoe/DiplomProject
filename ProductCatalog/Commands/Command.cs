using Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commands
{
    public abstract class Command : IFailable
    {
        public string TraceId { get; set; }
        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();

        public abstract void Execute();
    }
}
