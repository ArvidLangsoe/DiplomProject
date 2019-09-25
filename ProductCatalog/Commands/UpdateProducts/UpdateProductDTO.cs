﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.UpdateProducts
{
    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Discontinued { get; set; }

    }
}
