using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Storage.DTO
{
    public class ProductAvailabilityDTO
    {
        public Guid ProductId { get; set; }

        public int NumAvailable { get; set; }

    }
}
