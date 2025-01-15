using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator() {
            RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Product must have a valid price");
			RuleFor(x => x.Title).NotEmpty().WithMessage("Product must have a valid title");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Product must have a valid price");
		}
    }
}
