namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
	public class CreateOrderProfile:Profile
	{
		public CreateOrderProfile() {
			CreateMap<CreateOrderCommand, Order>();
			CreateMap<Order, CreateOrderResult> ();
		}
	}
}
