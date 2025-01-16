using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class GetAllCartsProfile:Profile
	{
		public GetAllCartsProfile() {
			CreateMap<GetAllCartsRequest, GetAllCartsQuery>();
			CreateMap<GetAllCartsResult, GetAllCartsResponse>()
		   .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
		}
	}
}
