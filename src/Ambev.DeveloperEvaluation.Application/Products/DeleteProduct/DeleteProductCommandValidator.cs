namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
	public class DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
		}
	}
}
