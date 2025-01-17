namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder
{
	public class UpdateOrderRequest
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public List<OrderItemCommandDto> Items { get; set; } = new();
	}
}
