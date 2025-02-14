namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
	public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId must be informed");
			RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch must be informed");
			RuleFor(x => x.Items).NotEmpty();
			RuleFor(x => x.Items).ForEach(e => e.SetValidator(new OrderItemDtoValidator()));
		}
	}
}
