namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem
{
	public class CancelOrderItemRequest
	{
		public int OrderId { get; set; }
		public int OrderItemId { get; set; }
	}
}
