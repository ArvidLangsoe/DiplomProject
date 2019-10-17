using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Describes products that are not deleted in the productcatalog. This is cached from Productcatalog usually
    /// </summary>
    public class AvailableProduct
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Discontinued { get; set; }
    }
}
