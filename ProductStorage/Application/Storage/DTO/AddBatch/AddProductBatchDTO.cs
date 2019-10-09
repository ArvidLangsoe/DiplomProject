using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Storage.DTO.AddBatch
{
    public class AddProductBatchDTO
    {
        public Guid ProductId { get; set; }
        public int Amount { get; set; }

    }
}
