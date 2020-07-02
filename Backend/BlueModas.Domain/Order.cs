using System;
using System.Collections.Generic;

namespace BlueModas.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Payment { get; set; }
        public int Status { get; set; }
        public int ShippingAddressId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}