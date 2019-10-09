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


        public static EventDTO From(Event aEvent){
            return new EventDTO()
            {
                EventCounter = aEvent.EventCounter,
                ObjectId = aEvent.ObjectId,
                ObjectType = aEvent.ObjectType,
                Event = Enum.GetName(typeof(EventType), aEvent.EventType),
                CreationDate = aEvent.CreationDate
            };

        }
    }
}
