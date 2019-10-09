using Core.Common;
using Core.Persistence;
using Domain;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Event> Query(int eventCounter, int amount) {
            return _eventRepository.GetEvents(x => x.EventCounter>= eventCounter && x.EventCounter < eventCounter+amount);
        }

        public IEnumerable<Event> Query()
        {
            return Query(0, 100);
        }
    }
}
