using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.Handler
{
	public class UpdateProductHandlerTest
	{
		private readonly IProductRespository repository;
		private readonly IMapper mapper;
		private readonly UpdateProductHandler handler;
		public UpdateProductHandlerTest()
		{
			repository = Substitute.For<IProductRespository>();
			mapper = Substitute.For<IMapper>();
			handler = new(repository, mapper);
		}

		[Fact]
		public async Task UpdateProductHandler_Handle_ReturnsProduct()
		{

			//arrange
			var fakeData = new Faker<UpdateProductCommand>()
				.RuleFor(x => x.Id, f=> f.Random.Number(1, 100))
				.RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
				.RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
				.RuleFor(x => x.Image, f => f.Image.PicsumUrl())
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(x => x.Title, f => f.Commerce.ProductName())
				.RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
				.RuleFor(r => r.Rating, f => f.Random.Double(1, 5))
				.RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate() 
			);

			var mockData = fakeData.Generate();
			var product = Product.Create(mockData.Price, mockData.Title, mockData.Description, mockData.Category, mockData.Image,
			ProductRating.Of(mockData.Rating.Count, mockData.Rating.Rating));
			product.Id = mockData.Id;

			repository.UpdateProductAsync(Arg.Any<Product>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
			.Returns(Task.FromResult(product));

			mapper.Map<UpdateProductResult>(Arg.Any<Product>()).Returns(new UpdateProductResult
			{
				Id = mockData.Id,
				Category = mockData.Category,
				Description = mockData.Description,
				Image = mockData.Image,
				Price = mockData.Price,
				Rating = mockData.Rating,
				Title = mockData.Title
			});
			//act

			var result = await handler.Handle(mockData, default!);

			//assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<UpdateProductResult>();
			result.Title.Should().Be(mockData.Title);
			result.Price.Should().Be(mockData.Price);
			result.Category.Should().Be(mockData.Category);
		}
		[Fact]
		public async Task UpdateProductHandler_Handle_ThrowsException()
		{

			//arrange
			var fakeData = new Faker<UpdateProductCommand>()
				.RuleFor(x => x.Id, f => f.Random.Number(1, 100))
				.RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
				.RuleFor(x => x.Category, f => f.Commerce.ProductMaterial())
				.RuleFor(x => x.Image, f => f.Image.PicsumUrl())
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price(-800M, -1M)))
				.RuleFor(x => x.Title, f => f.Commerce.ProductName())
				.RuleFor(x => x.Rating, f => new Faker<ProductRatingDto>()
				.RuleFor(r => r.Rating, f => f.Random.Double(1, 5))
				.RuleFor(r => r.Count, f => f.Random.Int(0, 1000)).Generate()
			);

			var mockData = fakeData.Generate();
			//act

			var result = async() => await handler.Handle(mockData, default!);

			//assert
			await result.Should().ThrowAsync<FluentValidation.ValidationException>();
		}
	}
}
