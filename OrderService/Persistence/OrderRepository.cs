using Application.Interfaces.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext _context;
        OrderRepository(OrderDbContext context)
        {
            _context = context;

        }

        public void AddOrder(Order order)
        {
            
        }
    }
}
