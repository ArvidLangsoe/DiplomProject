using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public interface IFailable
    {
        bool IsSuccesful { get; set; }

        List<Error> Errors { get; set; }
    }
}
