using Core.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {

        private ProductDbContext _dbContext;

        public EventRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEvent(Event eventInstance)
        {
            _dbContext.Events.Add(eventInstance);
            _dbContext.SaveChanges();
        }

        public void GetEvents(Expression<Func<Event, bool>> condition)
        {
            //Waiting to implement this until requirements are clear.
            throw new NotImplementedException();
        }
    }
}
