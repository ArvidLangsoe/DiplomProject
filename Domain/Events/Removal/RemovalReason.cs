using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum RemovalReason
    {
        Delivered = 0, //Item  was delivered
        Broken = 1, //Item broke during handling
        Missing = 2, //Item is gone from storage, stolen or just not able to be found.
        Deprecated = 3 //Item has passed its expiry date.
    }
}
