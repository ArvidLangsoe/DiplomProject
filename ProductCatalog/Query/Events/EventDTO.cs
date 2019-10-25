using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queries.Events
{
    public class EventDTO
    {
        public long EventCounter { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Event { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Guid TraceId { get; set; }


        public static EventDTO From(Event myEvent){
            return new EventDTO()
            {
                EventCounter = myEvent.EventCounter,
                ObjectId = myEvent.ObjectId,
                ObjectType = myEvent.ObjectType,
                Event = Enum.GetName(typeof(EventType), myEvent.EventType),
                CreationDate = myEvent.CreationDate,
                TraceId = myEvent.TraceId
            };

        }
    }
}
