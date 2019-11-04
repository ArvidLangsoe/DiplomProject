using Application.Interfaces.Persistence;
using Application.Queries.QueryOrders;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries
{
    public class OrderQuery
    {
        private IOrderRepository _orderRepository;


        public IEnumerable<OrderDTO> Result;


        public OrderQuery(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Query() {

            IEnumerable<Order> orders = _orderRepository.GetAllOrders();

            Result = orders.Select(o => OrderDTO.From(o));
        }


    }
}
