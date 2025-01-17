namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder
{
	public class UpdateOrderRequestValidator:AbstractValidator<UpdateOrderRequest>	
	{
		public UpdateOrderRequestValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
			RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId must be informed");
			RuleFor(x => x.Items).ForEach(e => e.SetValidator(new OrderItemDtoValidator()));
		}
	}
}
