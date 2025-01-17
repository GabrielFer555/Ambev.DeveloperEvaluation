namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder
{
	public class UpdateOrderResponse
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
		public List<OrderItemResponseDto> Items { get; set; } = new();
	}
}
