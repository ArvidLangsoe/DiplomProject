using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Persistence
{
    public interface IEventRepository
    {

        void AddEvent(Event eventInstance);

        void GetEvents(Expression<Func<Event, bool>> condition);
    }
}
