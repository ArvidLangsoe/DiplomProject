using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class OrderedProduct
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }


        [ForeignKey("Order")]
        public Guid OrderId { get; set;}
        public Order Order { get; set; }

    }
}
