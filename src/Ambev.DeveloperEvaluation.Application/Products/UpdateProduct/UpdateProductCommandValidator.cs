namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
	public class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator() {
			RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Product must have a valid price");
			RuleFor(x => x.Title).NotEmpty().WithMessage("Product must have a valid title");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Product must have a valid price");
		}
	}
}
