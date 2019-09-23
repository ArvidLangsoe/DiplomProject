using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Discontinued { get; set; }

        public bool Deleted { get; set; }

    }
}
