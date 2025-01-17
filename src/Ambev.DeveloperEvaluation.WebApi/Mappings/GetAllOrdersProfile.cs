using Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class GetAllOrdersProfile:Profile
	{
		public GetAllOrdersProfile() {
			CreateMap<GetAllOrdersRequest, GetAllOrdersQuery>();
			CreateMap<GetAllOrdersResult, GetAllOrdersResponse>();
		}
	}
}
