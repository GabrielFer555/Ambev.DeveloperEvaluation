namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts
{
	public class GetAllProductsResult
	{
		public IEnumerable<Product> Data { get; set; } = default!;
		public int Page {  get; set; }
        public int Limit { get; set; }
        public int TotalPages { get; set; }

    }
}
