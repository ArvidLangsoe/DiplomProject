using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ProductAddition : ProductEvent
    {
        public AdditionReason Reason { get; set; }
        public string Comment { get; set; }
    }
}
