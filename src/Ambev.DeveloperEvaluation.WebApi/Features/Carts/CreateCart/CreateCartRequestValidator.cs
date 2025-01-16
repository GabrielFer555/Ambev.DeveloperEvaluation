namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
	public class CreateCartRequestValidator:AbstractValidator<CreateCartRequest>
	{
		public CreateCartRequestValidator() {
			RuleFor(x => x.Products).NotEmpty().WithMessage("Cart cannot be empty");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
			RuleFor(x => x.Products).ForEach(e => e.SetValidator(new CartItemDtoValidator()));
		}
	}
}
