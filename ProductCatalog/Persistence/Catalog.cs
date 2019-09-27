using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Search;
using ProductCatalog;
using Queries.Products;

namespace Core.Persistence
{
    public class Catalog<T> : ICatalog<T> where T: ISearchable
    {
        private static int DEFAULT_PAGESIZE = 100;
        private static int MAX_REQUEST = 10000;
        private IQueryable<T> _items;

        public Catalog(IQueryable<T> items) {
            _items = items;
        }

        public CatalogPage<T> GetFront()
        {
            return GetFront(DEFAULT_PAGESIZE);
        }

        public CatalogPage<T> GetFront(int amount)
        {
            return GetFront(amount, new SearchParameters());
        }

        public CatalogPage<T> GetFront(SearchParameters searchParameters)
        {
            return GetFront(DEFAULT_PAGESIZE, searchParameters);
        }

        public CatalogPage<T> GetFront(int amount, SearchParameters searchParameters)
        {
            var query = _items;
            if (amount > MAX_REQUEST) {
                throw new ArgumentException("Request amount to large. Reduce the amount of items you query.");
            }

            var searchString = searchParameters.SearchString;

            if (!string.IsNullOrWhiteSpace(searchString)) {
                query = FilterBySearchString(query,searchString);
            }

            var limitedQuery = query.Take(amount);
            var finalQuery = limitedQuery.ToList();

            //TODO: Whatever is necesary to get the next page. 

            return new CatalogPage<T>(finalQuery,null);
        }


        public CatalogPage<T> GetNext(int amount, object keyNext)
        {
            throw new NotImplementedException();
        }


        private IQueryable<T> FilterBySearchString(IQueryable<T> searchables, string searchString) {
            return searchables.Where(x => x.Match(searchString)>=0).OrderBy(x => x.Match(searchString)); 
        }
    }
}
