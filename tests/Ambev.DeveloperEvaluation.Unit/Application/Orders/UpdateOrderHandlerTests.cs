using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

namespace Ambev.DeveloperEvaluation.Unit.Application.Orders
{
	public class UpdateOrderHandlerTests
	{
		private readonly IMapper mapper;
		private readonly IOrdersRepository ordersRepository;
		private readonly UpdateOrderHandler handler;

		public UpdateOrderHandlerTests()
		{

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddMaps(typeof(ApplicationLayer).Assembly);
			});
			mapper = config.CreateMapper();

			ordersRepository = Substitute.For<IOrdersRepository>();
			handler = new UpdateOrderHandler(ordersRepository, mapper);
		}

		[Fact]
		public async Task Handle_UpdateOrder_Then_ReturnsObject()
		{
			//arrange
			var fakeData = new Faker<UpdateOrderCommand>()
			.RuleFor(x => x.Id, f => 1)
		   .RuleFor(x => x.CustomerId, f => f.Random.Guid())
		   .RuleFor(x => x.Branch, f => f.Company.CompanyName()).Generate();

			var order = new Faker<Order>()
			.RuleFor(x => x.Id, f=> 1)
			.RuleFor(x => x.CustomerId, f => f.Random.Guid())
			.RuleFor(x => x.Branch, f => f.Company.CompanyName())
			.RuleFor(x => x.Items, f => new Faker<OrderItem>()
				.RuleFor(x => x.OrderId, f => 1)
				.RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(x => x.Discount, f=> 0)
				.RuleFor(x => x.Quantity, f => 3)
				.RuleFor(x => x.ProductId, f => f.Random.Int(0, 40))
				.Generate(1) 
			).Generate();

			var expectedOrder = new Faker<Order>()
				.RuleFor(x => x.Id, fakeData.Id)
				.RuleFor(x => x.CustomerId, f => fakeData.CustomerId)
				.RuleFor(x => x.Branch, f => fakeData.Branch)
				.RuleFor(x => x.Items, f => order.Items).Generate();

			ordersRepository.UpdateOrder(Arg.Any<Order>(), cancellation: default!).Returns(expectedOrder);


			//act
			var result = await handler.Handle(fakeData, default!);

			//assert

			result.Should().NotBeNull();
			result.Should().BeAssignableTo<UpdateOrderResult>();
			result.Id.Should().Be(fakeData.Id);
			result.Branch.Should().Be(fakeData.Branch);
			result.CustomerId.Should().Be(fakeData.CustomerId);
			

			await ordersRepository.Received(1).UpdateOrder(Arg.Any<Order>(), cancellation: default!);
		}

		[Fact]
		public async Task Handle_UpdateInvalidOrder_Then_ThrowsException()
		{
			//arrange

			var fakeData = new Faker<UpdateOrderCommand>()
			.RuleFor(x => x.Id, f => 1)
		   .RuleFor(x => x.CustomerId, f => Guid.Empty)
		   .RuleFor(x => x.Branch, f => f.Company.CompanyName()).Generate();

			//act

			var act = async () => await handler.Handle(fakeData, default!);
			
			//assert
			await act.Should().ThrowAsync<FluentValidation.ValidationException>();
			
		}
	}
}
