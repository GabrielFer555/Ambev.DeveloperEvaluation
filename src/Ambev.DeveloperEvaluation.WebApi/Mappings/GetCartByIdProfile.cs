using Ambev.DeveloperEvaluation.Application.Carts.GetCartById;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class GetCartByIdProfile:Profile
	{
		public GetCartByIdProfile()
		{
			CreateMap<GetCartByIdRequest, GetCartByIdQuery>();
			CreateMap<GetCartByIdResult, GetCartByIdResponse>();
		}
	}
}
