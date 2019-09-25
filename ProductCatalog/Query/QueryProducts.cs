using Core.Common;
using Core.Persistence;
using ProductCatalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries
{
    public class QueryProducts : IFailable
    {
        IProductRepository _productRepository;

        public QueryProducts(IProductRepository productRepository) {
            _productRepository = productRepository;
        }

        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();

        public List<ProductDTO> Query() {
            IEnumerable<Product> products = _productRepository.GetProducts();
            return products.Select(product => ProductDTO.From(product)).ToList();
        }
    }
}
