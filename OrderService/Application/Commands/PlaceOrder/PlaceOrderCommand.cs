using Application.Interfaces.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.PlaceOrder
{
    public class PlaceOrderCommand : Command
    {
        private IUnitOfWork _unitOfWork;
        private IOrderRepository _orderRepository;

        public PlaceOrderDTO Order { get; set; }

        public PlaceOrderCommand(IUnitOfWork unitOfWork, IOrderRepository orderRepository) {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public override void Execute()
        {
            var order = new Order() {
                OrderTime = DateTime.Now,
                Id = Guid.NewGuid()
            };

            foreach(Entry entry in Order.ProductOrders){
                var productOrder = new OrderedProduct() {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    Amount = entry.Amount,
                    ProductId = entry.Id
                };
                order.ProductOrders.Add(productOrder);
            }
            _orderRepository.AddOrder(order);
            _unitOfWork.CommitChanges();
        }
    }
}
