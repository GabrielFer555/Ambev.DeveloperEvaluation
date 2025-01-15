
namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
	public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Product must have a valid price");
			RuleFor(x => x.Title).NotEmpty().WithMessage("Product must have a valid title");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Product must have a valid price");
		}
	}
}
