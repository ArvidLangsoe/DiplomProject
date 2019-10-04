using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProductRemoval : ProductEvent
    {
        public RemovalReason Reason{ get; set;}
        public string Comment { get; set; }

    }
}
