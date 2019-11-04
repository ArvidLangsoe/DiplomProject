using Application.Interfaces.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context;

        }

        public void AddOrder(Order order)
        {
            _context.Add(order);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(x => x.ProductOrders).ToList();
        }
    }
}
