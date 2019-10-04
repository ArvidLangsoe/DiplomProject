using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ProductStorage
    {
        private IEnumerable<ProductBatch> _batchesOfProduct;

        private IEnumerable<ProductAddition> _addedProducts;

        private IEnumerable<ProductRemoval> _removedProducts;

        public Guid ProductId { get; set; }

        public ProductStorage(IEnumerable<ProductBatch> batchesOfProduct, IEnumerable<ProductAddition> addedProducts, IEnumerable<ProductRemoval> removedProducts) {
            _batchesOfProduct = batchesOfProduct;
            _addedProducts = addedProducts;
            _removedProducts = removedProducts;
        }

        public IEnumerable<ProductBatch> BatchesOfProduct {
            get {
                return _batchesOfProduct.OrderBy(x => x.Batch.Created);
            }
        }

        public int NumAvailableProduct
        {
            get
            {
                var totalStored = _batchesOfProduct.Sum(x => x.Amount);
                var totalAdded = _addedProducts.Count();
                var totalRemoved = _removedProducts.Count();
                var totalAvailable = totalStored + totalAdded - totalRemoved;

                return totalAvailable;
            }

        }


        public IEnumerable<ProductEvent> Events { get {
                var events = new List<ProductEvent>();
                events.AddRange(_addedProducts);
                events.AddRange(_removedProducts);

                return events.OrderBy(x => x.Created);
            }
        }
    }
}
