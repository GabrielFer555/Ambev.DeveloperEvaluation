namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdProfile:Profile
	{
		public GetCartByIdProfile() {
			CreateMap<Cart, GetCartByIdResult>();
		}
	}
}
