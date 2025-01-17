
namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdProfile:Profile
	{
		public GetOrderByIdProfile() { 
			CreateMap<Order, GetOrderByIdResult>();
		}
	}
}
