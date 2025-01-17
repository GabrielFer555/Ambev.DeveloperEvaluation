namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrderById
{
	public class GetOrderByIdValidator:AbstractValidator<GetOrderByIdRequest>	
	{
		public GetOrderByIdValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
