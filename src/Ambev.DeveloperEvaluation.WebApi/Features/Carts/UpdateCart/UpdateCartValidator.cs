namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
	public class UpdateCartValidator:AbstractValidator<UpdateCartRequest>
	{
		public UpdateCartValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id cannot be empty");
			RuleFor(x => x.Products).NotEmpty().WithMessage("Cart cannot be empty");
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required");
			RuleFor(x => x.Products).ForEach(e => e.SetValidator(new CartItemDtoValidator()));
		}
	}
}
