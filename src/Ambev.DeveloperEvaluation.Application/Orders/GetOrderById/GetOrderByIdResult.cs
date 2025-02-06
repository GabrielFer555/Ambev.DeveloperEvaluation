namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdResult
	{
		public int Id { get; set; }
		public OrderStatus OrderStatus { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
		public List<OrderItemResponseDto> Items { get; set; } = new();
		public decimal TotalPrice { get; set; }
		public decimal TotalAmountDiscount { get; set; }
	}
}
