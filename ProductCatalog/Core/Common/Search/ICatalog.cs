using ProductCatalog;
using Queries.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Persistence
{
    public interface ICatalog<T>
    {
        CatalogPage<T> GetFront();
        CatalogPage<T> GetFront(int amount);
        CatalogPage<T> GetFront(SearchParameters searchParameters);
        CatalogPage<T> GetFront(int amount, SearchParameters searchParameters);

        CatalogPage<T> GetNext(int amount, object keyNext);

    }

    public class CatalogPage<T> {

        public IEnumerable<T> Items { get; }

        //KeyNext is a key that allows one to get the next page. This has not been implemented yet.
        public object KeyNext { get; }

        public int Size {
            get {
                return Items.Count();
            }
        }


        public CatalogPage(IEnumerable<T> items, object keyNext)
        {
            Items = items;
            KeyNext = keyNext;
        }

    }

}
