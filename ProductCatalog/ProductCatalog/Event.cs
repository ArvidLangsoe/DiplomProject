using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Event
    {
        [Key]
        public long EventCounter { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public EventType EventType { get; set; }

    }

    public enum EventType
    {
        Added,
        Updated,
        Deleted
    }
}
