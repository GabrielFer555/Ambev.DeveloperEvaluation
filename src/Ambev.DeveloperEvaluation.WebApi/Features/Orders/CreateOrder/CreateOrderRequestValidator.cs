
namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
	public class CreateOrderRequestValidator:AbstractValidator<CreateOrderRequest>
	{
		public CreateOrderRequestValidator() {
			RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId must be informed");
			RuleFor(x => x.Items).ForEach(e => e.SetValidator(new OrderItemDtoValidator()));
			RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch must be informed");
		}
	}
}
