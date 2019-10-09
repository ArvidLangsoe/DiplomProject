using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Storage.DTO.AddBatch
{
    public class AddBatchDTO
    {
        public IEnumerable<AddProductBatchDTO> Products { get; set; } = new List<AddProductBatchDTO>();

        //TODO: Maybe use creator pattern here instead
        public Batch ToBatch()
        {
            var batch = new Batch()
            {
                Id = new Guid(),
                Created = DateTime.Now,
                Products = this.Products?.Select(x =>
                    new ProductBatch()
                    {
                        Id = new Guid(),
                        ProductId = x.ProductId,
                        Amount = x.Amount,
                    }
            ).ToList()
            };
            return batch;
        }
    }
}
