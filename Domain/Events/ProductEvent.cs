using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public abstract class ProductEvent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductBatchId { get; set; }
        public ProductBatch ProductBatch { get; set; }
        public DateTime Created { get; set; }

    }
}
