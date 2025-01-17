using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class CancelOrderProfile:Profile
	{
		public CancelOrderProfile() {
			CreateMap<CancelOrderRequest, CancelOrderCommand>();
			CreateMap<CancelOrderResult, CancelOrderResponse>();
			
		}
	}
}
