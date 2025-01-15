
namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsQuery:IRequest<GetAllProductsResult>
	{
        public int? _Page { get; set; }
		public int? _Limit { get; set; }

    }
}
