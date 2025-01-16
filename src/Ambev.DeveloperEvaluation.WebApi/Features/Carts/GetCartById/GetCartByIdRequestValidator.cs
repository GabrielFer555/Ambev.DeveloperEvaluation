namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById
{
	public class GetCartByIdRequestValidator:AbstractValidator<GetCartByIdRequest>
	{
		public GetCartByIdRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Cart Id must be informed");
		}
	}
}
