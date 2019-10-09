using Core.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Event> GetEvents(Expression<Func<Event, bool>> condition)
        {
            return _dbContext.Events.Where(condition).ToList();
        }
    }
}
