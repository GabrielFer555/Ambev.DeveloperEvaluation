using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetCategoriesProfile : Profile
    {
        public GetCategoriesProfile()
        {
            CreateMap<GetCategoriesResult, GetCategoriesResponse>();
        }
    }
}
