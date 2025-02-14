using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Integration.Orders.Utility
{
    public record OrderResponseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
        public string Branch { get; set; } = string.Empty;
    }

    public record OrderItemResponseDto()
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public int Discount { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public decimal TotalDiscount { get; set; }
    }
    public record GetOrderResponseDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; } = string.Empty ;
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
        public decimal TotalAmountDiscount { get; set; }
    }
}
