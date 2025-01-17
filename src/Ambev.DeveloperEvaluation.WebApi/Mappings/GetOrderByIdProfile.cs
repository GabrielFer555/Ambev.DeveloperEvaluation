using Ambev.DeveloperEvaluation.Application.Orders.GetOrderById;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrderById;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class GetOrderByIdProfile:Profile
	{
        public GetOrderByIdProfile()
		{
			CreateMap<GetOrderByIdRequest, GetOrderByIdQuery>();
			CreateMap<GetOrderByIdResult, GetOrderByIdResponse>();
		}
	}
}
