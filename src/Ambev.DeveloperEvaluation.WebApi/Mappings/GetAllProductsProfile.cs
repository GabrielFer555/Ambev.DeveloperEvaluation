using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<GetAllProductsRequest, GetAllProductsQuery>();
            CreateMap<GetAllProductsResult, GetAllProductsResponse>();
        }
    }
}
