using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
	public class UpdateCartProfile:Profile
	{
		public UpdateCartProfile() {
			CreateMap<UpdateCartCommand, Cart>();
			CreateMap<Cart, UpdateCartResult>();
		}
	}
}
