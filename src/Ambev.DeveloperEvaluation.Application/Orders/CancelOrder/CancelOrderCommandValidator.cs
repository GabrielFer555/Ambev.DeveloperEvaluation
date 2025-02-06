namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder
{
	public class CancelOrderCommandValidator:AbstractValidator<CancelOrderCommand>
	{
		public CancelOrderCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
