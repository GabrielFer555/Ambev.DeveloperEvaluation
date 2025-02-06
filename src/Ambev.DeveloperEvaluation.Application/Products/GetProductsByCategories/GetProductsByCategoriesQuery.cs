namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesQuery:IRequest<GetProductsByCategoriesResult>
	{
        public int? _Page { get; set; }
		public int? _Limit { get; set; }
		public string Category { get; set; } = default!;
	}
}
