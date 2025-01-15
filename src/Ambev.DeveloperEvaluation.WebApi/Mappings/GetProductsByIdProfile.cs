using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetProductsByIdProfile : Profile
    {
        public GetProductsByIdProfile()
        {
            CreateMap<GetProductsByIdRequest, GetProductsByIdQuery>();
            CreateMap<GetProductsByIdResult, GetProductsByIdResponse>();

        }
    }
}
