namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemCommand:IRequest<CancelOrderItemResult>
	{
        public int OrderId { get; set; }
		public int OrderItemId { get; set; }
	}
}
