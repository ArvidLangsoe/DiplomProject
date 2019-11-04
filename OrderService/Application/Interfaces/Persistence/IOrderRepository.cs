using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        IEnumerable<Order> GetAllOrders();
    }
}
