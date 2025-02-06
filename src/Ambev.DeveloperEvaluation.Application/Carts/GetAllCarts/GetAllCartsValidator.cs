namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts
{
	public class GetAllCartsValidator:AbstractValidator<GetAllCartsQuery>
	{
		public GetAllCartsValidator() {
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
		}
	}
}
