namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
	public class UpdateProductRequest
	{
		public int? Id { get; set; }
		public decimal Price { get; set; }
		public string Title { get; set; } = default!;

		public string Description { get; set; } = default!;

		public string Category { get; set; } = default!;
		public string Image { get; set; } = default!;
		public ProductRatingDto Rating { get; set; } = default!;
	}
}
