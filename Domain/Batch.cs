using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Batch
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Delivered { get; set; } = null;

        public List<ProductBatch> Products { get; set; }

    }
}
