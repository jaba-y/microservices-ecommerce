
using System;
using System.Collections.Generic;

namespace Ecommerce.Api.Orders.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Total { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}