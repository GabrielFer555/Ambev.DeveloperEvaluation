using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
	public class CreateProductProfile:Profile
	{
		public CreateProductProfile() {
			CreateMap<CreateProductCommand, Product>().ForMember(x => x.ProductRating, opt => opt.MapFrom(src => src.Rating));
			CreateMap<Product, CreateProductResult>().ForMember(x => x.Rating, opt => opt.MapFrom(src => src.ProductRating));
		}
	}
}
