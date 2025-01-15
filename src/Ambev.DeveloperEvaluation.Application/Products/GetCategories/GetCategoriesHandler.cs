
namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories
{
	public class GetCategoriesHandler (IProductRespository repository) : IRequestHandler<GetCategoriesQuery, GetCategoriesResult>
	{
		public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
		{
			var result = await repository.GetProductCategoriesAsync(cancellationToken);
			return new GetCategoriesResult
			{
				Categories = result
			};
		}
	}
}
