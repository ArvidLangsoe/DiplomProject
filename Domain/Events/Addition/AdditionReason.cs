using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum AdditionReason
    {
        Returned = 0, // Customer returned a product
        Found = 1, // Product found on storage. Usually missing items.
    }
}
