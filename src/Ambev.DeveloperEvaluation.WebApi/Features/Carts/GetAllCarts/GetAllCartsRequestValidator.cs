namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts
{
	public class GetAllCartsRequestValidator:AbstractValidator<GetAllCartsRequest>
	{
		public GetAllCartsRequestValidator() {
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
		}
	}
}
