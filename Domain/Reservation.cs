using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Amount { get; set; }

    }
}
