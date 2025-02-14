using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.Integration.Orders.TestData
{
    public static class CreateOrderTestData
    {
        private static Faker<CreateOrderCommand> fakeData = new Faker<CreateOrderCommand>()
            .RuleFor(x => x.CustomerId, f => f.Random.Uuid())
            .RuleFor(x => x.Branch, f => f.Commerce.Department())
            .RuleFor(x => x.Items, f =>
                new Faker<OrderItemCommandDto>().RuleFor(x => x.ProductId, f => f.Random.Int(1, 3))
                .RuleFor(x => x.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 20)).Generate(2)
            );
 
            
    }
}
