using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ProductBatch
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BatchId { get; set; }
        public Batch Batch { get; set; }

        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}
