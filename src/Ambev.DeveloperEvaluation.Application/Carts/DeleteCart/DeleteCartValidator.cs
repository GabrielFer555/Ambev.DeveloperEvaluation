namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
	public class DeleteCartValidator:AbstractValidator<DeleteCartCommand>
	{
		public DeleteCartValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("CartId must be Informed");
		}
	}
}
