namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
	public class UpdateCartCommandValidator:AbstractValidator<UpdateCartCommand>
	{
		public UpdateCartCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id cannot be empty");
			RuleFor(x => x.Products).NotEmpty().WithMessage("Cart cannot be empty");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
			RuleFor(x => x.Products).ForEach(e => e.SetValidator(new CartItemDtoValidator()));
		}
	}
}
