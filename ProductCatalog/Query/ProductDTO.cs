using ProductCatalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace Queries
{
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Discontinued { get; set; }


        public static ProductDTO From(Product product) {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Discontinued = product.Discontinued
            };
        }

    }
}