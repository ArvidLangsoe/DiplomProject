using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries.QueryOrders
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public IEnumerable<ProductOrderDTO> OrderedProducts { get; set; } = new List<ProductOrderDTO>();


        public static OrderDTO From(Order order)
        {
            return new OrderDTO() {
                Id = order.Id,
                OrderTime = order.OrderTime,
                OrderedProducts = order.ProductOrders.Select(po => new ProductOrderDTO() {
                    Amount = po.Amount,
                    ProductId = po.ProductId
                })
            };


        }
    }



}
