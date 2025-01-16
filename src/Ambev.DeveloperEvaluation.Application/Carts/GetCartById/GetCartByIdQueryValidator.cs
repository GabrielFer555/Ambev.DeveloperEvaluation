namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartById
{
	public class GetCartByIdQueryValidator:AbstractValidator<GetCartByIdQuery>
	{
		public GetCartByIdQueryValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id must be informed");
		}
	}
}
