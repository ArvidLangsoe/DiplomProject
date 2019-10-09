using ProductCatalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace Queries
{
    //TODO: Never used.
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Discontinued { get; set; }


        public static ProductDTO From(Product product) {
            return new ProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Discontinued = product.Discontinued
            };
        }

    }
}