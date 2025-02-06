namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
	public class UpdateProductProfile:Profile
	{
		public UpdateProductProfile() {
			CreateMap<UpdateProductCommand, Product>().ForMember(x => x.ProductRating, opt => opt.MapFrom(src => src.Rating));
			CreateMap<Product, UpdateProductResult>().ForMember(x => x.Rating, opt => opt.MapFrom(src => new ProductRatingDto(src.ProductRating.Count, src.ProductRating.Rating)));
		}
	}
}
