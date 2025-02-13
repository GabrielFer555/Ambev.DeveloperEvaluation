using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.Integration.Products.TestData
{
    public static class UpdateProductTestData
    {
        private static readonly Faker<UpdateProductCommand> FakeData = new Faker<UpdateProductCommand>()
            .RuleFor(x => x.Id, f => f.Random.Int())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
                .RuleFor(x => x.Image, f => f.Image.PicsumUrl())
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Title, f => f.Commerce.ProductName())
                .RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
                .RuleFor(r => r.Rating, f => f.Random.Double(1, 5))
                .RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate());

        public static UpdateProductCommand GenerateValidData()
        {
            return FakeData.Generate();
        }
    }
}
