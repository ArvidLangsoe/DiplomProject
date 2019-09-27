using Core.Common.Search;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog
{
    public class Product : ISearchable
    {

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Discontinued { get; set; }

        public bool Deleted { get; set; }

        public DateTime Creation { get; set; } = DateTime.Now;
        public DateTime LastUpdate { get; set; } = DateTime.Now;



        public Event ConstructEvent(EventType eventType) {
            return new Event()
            {
                ObjectId = Id,
                EventType = eventType,
                ObjectType = this.GetType().Name,
            };
        }

        public double Match(string searchString)
        {
            searchString = searchString.ToLower();
            bool descriptionHasWords = Description.ToLower().Contains(searchString);
            bool titleHasWords = Title.ToLower().Contains(searchString);
            return descriptionHasWords || titleHasWords ? 1 : -1;
        }
    }
}
