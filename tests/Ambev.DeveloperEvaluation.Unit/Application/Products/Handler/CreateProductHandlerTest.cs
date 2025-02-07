using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;


namespace Ambev.DeveloperEvaluation.Unit.Application.Products.Handler
{
	public class CreateProductHandlerTest
	{
		private readonly IProductRespository repository;
		private readonly CreateProductHandler handler;
		public CreateProductHandlerTest()
		{
			repository = Substitute.For<IProductRespository>();
			handler = Substitute.For<CreateProductHandler>(repository);
		}

		[Fact (DisplayName ="Given valid object, when create Product should return Product")]
		public async Task CreateProductHandlerTest_Handle_ShouldReturnProduct()
		{
			//arrange
				var fakeInput = new Faker<CreateProductCommand>().
				RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
				.RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
				.RuleFor(x => x.Image, f => f.Image.PicsumUrl())
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(x => x.Title, f=> f.Commerce.ProductName())
				.RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
				.RuleFor(r => r.Rating, f => f.Random.Double(1, 5)) // Rating between 1 and 5
				.RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate()  // Random number of reviews
			);

			
			var handlerMock = Substitute.ForPartsOf<CreateProductHandler>(repository);
			var mockData = fakeInput.Generate();
			var product = Product.Create(mockData.Price, mockData.Title, mockData.Description, mockData.Category, mockData.Image,
			ProductRating.Of(mockData.Rating.Count, mockData.Rating.Rating));

			 handlerMock.CreateNewProduct(Arg.Any<CreateProductCommand>())
			.Returns(product);

			 repository.CreateProductAsync(Arg.Any<Product>(), default!)
				.Returns(product);

			//act

			var result = await handler.Handle(mockData, default!);

			//assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<CreateProductResult>();
			result.Title.Should().Be(mockData.Title);
			result.Price.Should().Be(mockData.Price);
			result.Category.Should().Be(mockData.Category);
		}
		[Fact (DisplayName = "Given invalid object, when Create Product should throw exception")]
		public async Task CreateProductHandler_Handle_ShouldThrowException()
		{
			var invalidFakeInput = new Faker<CreateProductCommand>().
				RuleFor(x => x.Description,f => f.Commerce.ProductName())
				.RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
				.RuleFor(x => x.Image, f => f.Image.PicsumUrl())
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(x => x.Title, f => string.Empty)
				.RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
				.RuleFor(r => r.Rating, f => f.Random.Double(5, -1 )) // Rating between 1 and 5
				.RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate()  // Random number of reviews
			);

			var mockData = invalidFakeInput.Generate();

			//act

			var result = async() => await handler.Handle(mockData, default!);

			//assert

			await result.Should().ThrowAsync<FluentValidation.ValidationException>();


		}
	}
}
