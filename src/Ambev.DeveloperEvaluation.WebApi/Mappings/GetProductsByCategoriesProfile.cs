using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductsByCategoriesProfile : Profile
    {
        public GetProductsByCategoriesProfile()
        {
            CreateMap<GetProductsByCategoriesRequest, GetProductsByCategoriesQuery>();
            CreateMap<GetProductsByCategoriesResult, GetProductsByCategoriesResponse>();
        }
    }
}
