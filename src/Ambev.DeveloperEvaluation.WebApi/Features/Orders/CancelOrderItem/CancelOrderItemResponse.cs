﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem
{
	public class CancelOrderItemResponse
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Branch { get; set; } = string.Empty;
		public List<OrderItemResponseDto> Items { get; set; } = new();
		public OrderStatus OrderStatus { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal TotalAmountDiscount { get; set; }
	}
}
