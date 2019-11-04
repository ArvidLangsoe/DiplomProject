using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public List<OrderedProduct> ProductOrders { get; set; } = new List<OrderedProduct>();

    }
}
