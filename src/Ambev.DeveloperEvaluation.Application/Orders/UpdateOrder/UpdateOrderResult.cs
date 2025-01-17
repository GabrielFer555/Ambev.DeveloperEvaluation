namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
	public class UpdateOrderResult
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public List<OrderItemResponseDto> Items { get; set; } = new();
		public OrderStatus OrderStatus { get; set; }
	}
}
