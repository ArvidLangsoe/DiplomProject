using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class EventCounter
    {
        public string Id { get; set; }
        public int Value {get;set;} = 0;
    }
}
