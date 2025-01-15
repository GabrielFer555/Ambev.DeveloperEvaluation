using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
	public class GetProductByIdRequestValidator:AbstractValidator<GetProductsByIdRequest>
	{
		public GetProductByIdRequestValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id must be informed");
		}
	}
}
