using Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class CancelOrderItemProfile:Profile
	{
		public CancelOrderItemProfile() {
			CreateMap<CancelOrderItemRequest, CancelOrderItemCommand>();
			CreateMap<CancelOrderItemResult, CancelOrderItemResponse>();
		}
	}
}
