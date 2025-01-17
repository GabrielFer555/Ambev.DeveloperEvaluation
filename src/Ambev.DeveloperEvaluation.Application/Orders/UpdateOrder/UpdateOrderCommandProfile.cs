namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
	public class UpdateOrderCommandProfile:Profile
	{
		public UpdateOrderCommandProfile() {
			CreateMap<UpdateOrderCommand, Order>();
			CreateMap<Order, UpdateOrderResult>();
		}
	}
}
