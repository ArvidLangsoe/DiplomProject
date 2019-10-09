using Core.Common;
using Core.Persistence;
using Domain;
using Queries.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queries
{
    public class QueryEvents : IFailable
    {
        IEventRepository _eventRepository;

        public QueryEvents(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public bool IsSuccesful { get; set; } = true;
        public List<Error> Errors { get; set; } = new List<Error>();

        public IEnumerable<EventDTO> Query(int eventCounter, int amount) {
            IEnumerable<Event> events = _eventRepository.GetEvents(x => x.EventCounter>= eventCounter && x.EventCounter < eventCounter+amount);

            return events.Select(x => EventDTO.From(x));
        }

        public IEnumerable<EventDTO> Query()
        {
            return Query(0, 100);
        }
    }
}
