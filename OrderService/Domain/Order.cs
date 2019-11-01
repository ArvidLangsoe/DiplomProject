using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderedProduct> ProductOrders { get; set; }

    }
}
