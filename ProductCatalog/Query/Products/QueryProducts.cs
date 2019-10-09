using Core.Common;
using Core.Persistence;
using ProductCatalog;
using Queries.Products;
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

        public CatalogPage<Product> Query() {
            return _productRepository.GetCatalog().GetFront();

        }

        public CatalogPage<Product> Query(SearchParameters searchParameters)
        {
            return _productRepository.GetCatalog().GetFront(searchParameters);
        }


        public CatalogPage<Product> Query(List<Guid> ids)
        {
            return _productRepository.GetCatalog().GetSpecific(ids);

        }
    }
}
