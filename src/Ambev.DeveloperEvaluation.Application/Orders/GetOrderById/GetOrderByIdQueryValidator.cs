namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdQueryValidator:AbstractValidator<GetOrderByIdQuery>
	{
		public GetOrderByIdQueryValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
