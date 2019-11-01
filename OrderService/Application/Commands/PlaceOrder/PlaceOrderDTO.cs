using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PlaceOrder
{
    public class PlaceOrderDTO
    {
        public List<Entry> ProductOrders { get; set; }

    }


    public class Entry {
        public Guid Id { get; set; }
        public int Amount { get;  set; }
    }
}
