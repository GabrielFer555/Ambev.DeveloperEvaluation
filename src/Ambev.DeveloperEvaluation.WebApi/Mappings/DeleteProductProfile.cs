using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class DeleteProductProfile : Profile
    {
        public DeleteProductProfile()
        {
            CreateMap<DeleteProductRequest, DeleteProductCommand>();
            CreateMap<DeleteProductResult, DeleteProductResponse>();
        }
    }
}
