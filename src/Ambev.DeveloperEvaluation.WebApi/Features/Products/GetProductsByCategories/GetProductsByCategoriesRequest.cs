namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesRequest
	{
		public int? _Page { get; set; }
		public int? _Limit { get; set; }
		public string Category { get; set; } = default!;
	}
}
