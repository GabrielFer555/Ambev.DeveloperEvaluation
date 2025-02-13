using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Integration.Products.TestData
{
    public static class CreateProductTestData
	{
		private static readonly Faker<CreateProductRequest> FakeData = new Faker<CreateProductRequest>()
			.RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
				.RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
				.RuleFor(x => x.Image, f => f.Image.PicsumUrl())
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(x => x.Title, f=> f.Commerce.ProductName())
				.RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
				.RuleFor(r => r.Rating, f => f.Random.Double(1, 5)) 
				.RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate());

		public static CreateProductRequest GenerateValidData()
		{
			return FakeData.Generate();
		}

	}
}
