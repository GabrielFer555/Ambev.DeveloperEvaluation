namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders
{
	public class GetAllOrdersValidator:AbstractValidator<GetAllOrdersRequest>
	{
		public GetAllOrdersValidator() {
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
		}
	}
}
