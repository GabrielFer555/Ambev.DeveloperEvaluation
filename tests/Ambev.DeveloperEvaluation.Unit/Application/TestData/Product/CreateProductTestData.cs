using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Product
{
	public static class CreateProductTestData
	{
		private static readonly Faker<CreateProductCommand> createUserHandlerFaker = new Faker<CreateProductCommand>()
		.RuleFor(u => u.Category, f => f.Lorem.Word())
		.RuleFor(u => u.Description, f => f.Commerce.ProductName())
		.RuleFor(u => u.Price, f => Math.Round(f.Random.Decimal(10, 1000), 2))
		.RuleFor(u => u.Title, f => f.Commerce.ProductName())
		.RuleFor(u => u.Rating, f => new ProductRatingDto(f.Random.Number(), f.Random.Double(1, 5)))
		.RuleFor(u => u.Image, f => f.Image.DataUri(200, 500));
		public static CreateProductCommand GenerateValidCommand()
		{
			return createUserHandlerFaker.Generate();
		}
	}
}
