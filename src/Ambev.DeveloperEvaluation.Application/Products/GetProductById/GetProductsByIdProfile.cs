namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
	public class GetProductsByIdProfile:Profile
	{
		public GetProductsByIdProfile() { 
			CreateMap<Product, GetProductsByIdResult>().ForMember(x=> x.Rating, opt => opt.MapFrom(x=> x.ProductRating));
		}
	}
}
