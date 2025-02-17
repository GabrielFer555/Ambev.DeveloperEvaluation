using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

namespace Ambev.DeveloperEvaluation.Integration.Carts.TestData
{
	public static class CreateCartTestData
	{
		private static Faker<CreateCartRequest> fakeData = new Faker<CreateCartRequest>()
			.RuleFor(x => x.UserId, f => f.Random.Uuid())
			.RuleFor(x => x.Date, f => DateTime.UtcNow)
			.RuleFor(x => x.Products, f => new Faker<CartItemDto>()
			.RuleFor(x => x.ProductId, f => 1).RuleFor(x => x.Quantity, f => f.Random.Int(1 , 20)).Generate(1));

		public static CreateCartRequest GenerateValidData()
		{
			return fakeData.Generate();
		}
	}
}
