namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
	public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
			RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId must be informed");
		}
	}
}
