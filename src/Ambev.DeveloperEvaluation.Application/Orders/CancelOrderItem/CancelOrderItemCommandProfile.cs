namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemCommandProfile:Profile
	{
		public CancelOrderItemCommandProfile() {
			CreateMap<Order, CancelOrderItemResult>();	
		}
	}
}
