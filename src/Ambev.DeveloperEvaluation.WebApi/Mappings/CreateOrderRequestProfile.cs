using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class CreateOrderRequestProfile:Profile
	{
		public CreateOrderRequestProfile() {
			CreateMap<CreateOrderRequest, CreateOrderCommand>();
			CreateMap<CreateOrderResult, CreateOrderResponse>();
		}
	}
}
