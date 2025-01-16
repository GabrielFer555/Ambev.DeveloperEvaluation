using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
	public class DeleteCartProfile:Profile
	{
		public DeleteCartProfile() {
			CreateMap<DeleteCartRequest, DeleteCartCommand>();
			CreateMap<DeleteCartResult, DeleteCartResponse>();
		
		}
	}
}
