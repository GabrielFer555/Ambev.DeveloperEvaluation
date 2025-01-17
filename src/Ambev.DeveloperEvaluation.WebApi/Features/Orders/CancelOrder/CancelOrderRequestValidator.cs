namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder
{
	public class CancelOrderRequestValidator:AbstractValidator<CancelOrderRequest>	
	{
		public CancelOrderRequestValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
