using Ambev.DeveloperEvaluation.Application.Orders;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Application.Orders
{
	public class CreateOrderHandlerTests
    {

        private readonly IMapper mapper;
        private readonly IOrdersRepository ordersRepository;
        private readonly IProductRespository productRespository;
        private readonly IUserRepository userRepository;
        private readonly CreateOrderHandler handler;
        public CreateOrderHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationLayer).Assembly); 
            });
            mapper = config.CreateMapper();
        
            productRespository = Substitute.For<IProductRespository>();
            ordersRepository = Substitute.For<IOrdersRepository>();
            userRepository = Substitute.For<IUserRepository>();
            handler = new CreateOrderHandler(ordersRepository, productRespository, userRepository, mapper);
        }

        [Fact]
        public async Task Handle_ValidOrder_ReturnsOrder()
        {
            var numberOfProducts = 2;
            var fakeData = new Faker<CreateOrderCommand>()
            .RuleFor(x => x.CustomerId, f => f.Random.Guid())
            .RuleFor(x => x.Branch, f => f.Company.CompanyName())
            .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, f => f.Random.Int(11, 20))
                .RuleFor(x => x.ProductId, f => f.Random.Int(0, 40))
                .Generate(numberOfProducts) // Generate two items in the list
            );

            var mockData = fakeData.Generate();
            var orderCreated = mapper.Map<Order>(mockData);

            var fakeUser = new Faker<User>()
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Name, f => new Name(f.Name.FirstName(), f.Name.LastName()))
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Password, f => f.Internet.Password()) // Consider hashing this
                .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
                .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
                .RuleFor(u => u.CreatedAt, f => f.Date.Past())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent(10))
                .RuleFor(u => u.Address, f => new Faker<Address>()
                    .RuleFor(a => a.City, f => f.Address.City())
                    .RuleFor(a => a.Street, f => f.Address.StreetName())
                    .RuleFor(a => a.Number, f => f.Random.Int(1, 9999))
                    .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
                    .RuleFor(a => a.Long, f => f.Address.Longitude().ToString())
                    .RuleFor(a => a.Lat, f => f.Address.Latitude().ToString()).Generate());


            userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(fakeUser.Generate()));
            productRespository.GetProductByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Product?>(new Product { Id = 1 }));
            ordersRepository.CreateOrder(Arg.Any<Order>(), default!).Returns(Task.FromResult(orderCreated));

            //Act

            var result = await handler.Handle(mockData, default!);
            

            //assert    
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreateOrderResult>();
            result.Items.ForEach(x => x.TotalDiscount.Should().BeLessThan(x.TotalPrice));
            foreach (var item in result.Items)
            {
                int totalDiscount = 0;
                if (item.Quantity >= 4 && item.Quantity < 10) totalDiscount = 10;
                if (item.Quantity >= 10 && item.Quantity <=20) totalDiscount = 20;

                var priceWithDiscount = item.TotalPrice - (item.TotalPrice * (totalDiscount / 100m));

                item.TotalDiscount.Should().Be(priceWithDiscount);
            }


            result.Items.ForEach(x => x.TotalDiscount.Should().NotBe(0));
            result.Items.Should().HaveCount(numberOfProducts).And.ContainItemsAssignableTo<OrderItemResponseDto>().And.OnlyHaveUniqueItems();

            await userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>());
            await productRespository.Received(numberOfProducts).GetProductByIdAsync(Arg.Any<int>());

        }
        [Fact]
        public async Task Handle_OrderWithMoreItemsThanShould_ThrowException()
        {
            var fakeData = new Faker<CreateOrderCommand>()
            .RuleFor(x => x.CustomerId, f => f.Random.Guid())
            .RuleFor(x => x.Branch, f => f.Company.CompanyName())
            .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, f => f.Random.Int(21, 23))
                .RuleFor(x => x.ProductId, f => f.Random.Int(0, 40))
                .Generate(1) // Generate two items in the list
            );


            var result = async () => await handler.Handle(fakeData, default!);

            await result.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
        [Fact]
        public async Task Handle_OrderWithInvalidCustomer_ThrowException()
        {
            var fakeData = new Faker<CreateOrderCommand>()
            .RuleFor(x => x.CustomerId, f => f.Random.Guid())
            .RuleFor(x => x.Branch, f => f.Company.CompanyName())
            .RuleFor(x => x.Items, f => new Faker<OrderItemCommandDto>()
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, f => f.Random.Int(10, 20))
                .RuleFor(x => x.ProductId, f => f.Random.Int(0, 40))
                .Generate(1) // Generate two items in the list
            );

            userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult<User?>(null));

            var result = async () => await handler.Handle(fakeData, default!);

            await result.Should().ThrowAsync<BadRequestException>();
        }
    }
}
