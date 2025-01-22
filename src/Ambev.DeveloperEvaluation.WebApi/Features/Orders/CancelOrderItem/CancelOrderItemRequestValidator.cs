namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem
{
	public class CancelOrderItemRequestValidator:AbstractValidator<CancelOrderItemRequest>
	{
		public CancelOrderItemRequestValidator() {
			RuleFor(x => x.OrderItemId).NotEmpty();
			RuleFor(x => x.OrderId).NotEmpty();
		}
	}
}
