namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategories
{
	public class GetProductsByCategoriesResponse
	{
		public List<Product> Products { get; set; } = new List<Product>();
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
