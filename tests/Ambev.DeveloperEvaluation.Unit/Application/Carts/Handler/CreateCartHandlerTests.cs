using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.Handler
{
	public class CreateCartHandlerTests
	{
		private readonly CreateCartHandler handler;
		private readonly IUserRepository userRepository;
		private readonly ICartRepository cartRepository;
		private readonly MapperConfiguration mapperConfiguration;
		private readonly IMapper mapper;
		private readonly IProductRespository productRespository;

		public CreateCartHandlerTests()
		{
			cartRepository = Substitute.For<ICartRepository>();
			userRepository = Substitute.For<IUserRepository>();
			productRespository = Substitute.For<IProductRespository>();
			mapperConfiguration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ApplicationLayer).Assembly));
			mapper = new Mapper(mapperConfiguration);
			handler = new CreateCartHandler(cartRepository, mapper, userRepository, productRespository);


		}

		[Fact]
		public async Task Handler_ValidCart_ReturnsObject()
		{
			//arrange
			var fakeData = new Faker<CreateCartCommand>()
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
	.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f =>
				new Faker<CartItemDto>()
					.RuleFor(x => x.ProductId, f => f.Random.Int())
					.RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
					.Generate(3) // Correct way to generate a list
			);
			var mockCommand = fakeData.Generate();

			userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(new User()));
			productRespository.GetProductByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Product?>(new Product
			{
				Id = 1,
			})); //mocking product repository behaviour to always return something valid 

			var cart = mapper.Map<Cart>(mockCommand);

			cartRepository.CreateCartAsync(Arg.Any<Cart>()).Returns(cart);

			//act

			var result = await handler.Handle(mockCommand, default!);

			//assert

			result.Should().NotBeNull();
			result.Should().BeAssignableTo<CreateCartResult>();

			result.UserId.Should().Be(result.UserId);
			await productRespository.Received(3).GetProductByIdAsync(Arg.Any<int>());
			await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
		}
		[Fact]
		public async Task Handle_InvalidCart_ThrowsException()
		{
			//arrange
			var fakeData = new Faker<CreateCartCommand>() //mocking an invalid object 
			.RuleFor(x => x.UserId, f => Guid.Empty)
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f => new()).Generate();


			//act
			Func<Task> act = async () => await handler.Handle(fakeData, default!);

			//assert

			await act.Should().ThrowAsync<FluentValidation.ValidationException>();
		}


		[Fact]
		public async Task Handle_InvalidUser_ThrowsException()
		{
			//arrange
			var fakeData = new Faker<CreateCartCommand>()
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f =>
				new Faker<CartItemDto>()
					.RuleFor(x => x.ProductId, f => f.Random.Int())
					.RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
					.Generate(3) // Correct way to generate a list
			).Generate();
			userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(null));

			//act

			Func<Task> act = async () => await handler.Handle(fakeData, default!);

			//assert

			await act.Should().ThrowAsync<BadRequestException>();
		}

		[Fact]
		public async Task Handle_InvalidProduct_ThrowsException()
		{
			//arrange
			var fakeData = new Faker<CreateCartCommand>()
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f =>
				new Faker<CartItemDto>()
					.RuleFor(x => x.ProductId, f => f.Random.Int())
					.RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
					.Generate(3) // Correct way to generate a list
			).Generate();
			userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(new User()));
			productRespository.GetProductByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Product?>(null));

			//act
			Func<Task> act = async () => await handler.Handle(fakeData, default!);

			//assert

			await act.Should().ThrowAsync<BadRequestException>();
			await productRespository.Received(1).GetProductByIdAsync(Arg.Any<int>());
			await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
		}
	}
}
