using Core.Util.PatchProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commands.UpdateProducts
{
    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        public List<PropertyUpdate> Updates { get; set; }
    }
}
