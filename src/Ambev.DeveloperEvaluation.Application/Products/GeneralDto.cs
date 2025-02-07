namespace Ambev.DeveloperEvaluation.Application.Products
{
	public record ProductRatingDto {

		public int Count { get; set; }
		public double Rating { get; set; }
		public ProductRatingDto()
		{
			
		}

		public ProductRatingDto(int Count, double Rating) { 
			this.Count = Count;
			this.Rating = Rating;
		}
	}
}
