namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemCommandValidator:AbstractValidator<CancelOrderItemCommand>
	{
		public CancelOrderItemCommandValidator() {
			RuleFor(x => x.OrderItemId).NotEmpty();
			RuleFor(x => x.OrderId).NotEmpty();
		}
	}
}
