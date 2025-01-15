using System.Data;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
	public class UpdateProductRequestValidator:AbstractValidator<UpdateProductRequest>
	{
		public UpdateProductRequestValidator() {
			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Product must have a valid price");
			RuleFor(x => x.Title).NotEmpty().WithMessage("Product must have a valid title");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Product must have a valid price");
		}
	}
}
