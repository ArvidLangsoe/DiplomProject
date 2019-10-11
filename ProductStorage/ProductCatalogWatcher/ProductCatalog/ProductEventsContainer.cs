using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.ProductCatalogClient
{
    public class EventDTO
    {
        public long EventCounter { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string EventType { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;


    }
}
