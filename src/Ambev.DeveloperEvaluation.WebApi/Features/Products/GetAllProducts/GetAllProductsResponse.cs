namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
	public class GetAllProductsResponse
	{
		public IEnumerable<Product> Data { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
