﻿namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
	public class CreateOrderResult
	{
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }
}
