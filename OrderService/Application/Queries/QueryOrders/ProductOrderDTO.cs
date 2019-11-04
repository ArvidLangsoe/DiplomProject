using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.QueryOrders
{
    public class ProductOrderDTO
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
