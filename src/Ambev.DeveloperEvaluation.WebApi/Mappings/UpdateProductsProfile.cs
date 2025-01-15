using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class UpdateProductsProfile : Profile
    {
        public UpdateProductsProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();

        }
    }
}
