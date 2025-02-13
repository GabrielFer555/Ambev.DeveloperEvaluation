using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.Handler
{
	public class UpdateCartHandlerTests
	{
		private readonly UpdateCartHandler handler;
		private readonly IUserRepository userRepository;
		private readonly ICartRepository cartRepository;
		private readonly MapperConfiguration mapperConfiguration;
		private readonly IMapper mapper;
		private readonly IProductRespository productRespository;
		public UpdateCartHandlerTests()
		{
			cartRepository = Substitute.For<ICartRepository>();
			userRepository = Substitute.For<IUserRepository>();
			productRespository = Substitute.For<IProductRespository>();
			mapperConfiguration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ApplicationLayer).Assembly));
			mapper = new Mapper(mapperConfiguration);
			handler = new(mapper, cartRepository, productRespository, userRepository);
		}

		[Fact]
		public async Task Handler_ValidCart_ReturnsObject()
		{
			var fakeData = new Faker<UpdateCartCommand>()
			.RuleFor(x => x.Id, f => f.Random.Int())
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f =>
				new Faker<CartItemDto>()
					.RuleFor(x => x.ProductId, f => f.Random.Int())
					.RuleFor(x => x.Quantity, f => f.Random.Int(1, 10))
					.Generate(3) // Correct way to generate a list
			).Generate();

			userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(new User()));
			productRespository.GetProductByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Product?>(new Product
			{
				Id = 1,
			}));
			var cart = mapper.Map<Cart>(fakeData);

			cartRepository.UpdateCartAsync(Arg.Any<Cart>()).Returns(cart);

			//act

			var result = await handler.Handle(fakeData, default!);

			//assert

			result.Should().NotBeNull();
			result.Should().BeAssignableTo<UpdateCartResult>();

			result.UserId.Should().Be(result.UserId);
			await productRespository.Received(3).GetProductByIdAsync(Arg.Any<int>());
			await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

		}

		[Fact]
		public async Task Handler_InvalidCart_ThrowsException()
		{
			var fakeData = new Faker<UpdateCartCommand>()
			.RuleFor(x => x.Id, f => f.Random.Int())
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f => new()
			).Generate();

			//act

			var result = async () => await handler.Handle(fakeData, default!);

			//assert

			await result.Should().ThrowAsync<FluentValidation.ValidationException>();

			await productRespository.DidNotReceive().GetProductByIdAsync(Arg.Any<int>());
			await userRepository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

		}

		[Fact]
		public async Task Handler_InvalidUser_ThrowsException()
		{
			var fakeData = new Faker<UpdateCartCommand>()
			.RuleFor(x => x.Id, f => f.Random.Int())
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

			var result = async () => await handler.Handle(fakeData, default!);

			//assert

			await result.Should().ThrowAsync<BadRequestException>();

			await productRespository.DidNotReceive().GetProductByIdAsync(Arg.Any<int>());
			await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

		}
		[Fact]
		public async Task Handler_InvalidProduct_ThrowsException()
		{
			var fakeData = new Faker<UpdateCartCommand>()
			.RuleFor(x => x.Id, f => f.Random.Int())
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

			var result = async () => await handler.Handle(fakeData, default!);

			//assert

			await result.Should().ThrowAsync<BadRequestException>();

			await productRespository.Received(1).GetProductByIdAsync(Arg.Any<int>());
			await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());

		}
	}
}
