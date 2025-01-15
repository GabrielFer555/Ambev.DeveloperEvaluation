using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
	public class DeleteProductHandler (IProductRespository repository): IRequestHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			DeleteProductCommandValidator validator = new();
			var validBody = await validator.ValidateAsync(request, cancellationToken);
			if (!validBody.IsValid) throw new ValidationException(validBody.Errors);

			var result = await repository.DeleteProductAsync(request.Id, cancellationToken);
			string message = result == true ? "Product deleted successfully" : "Error deleting product";
			return new DeleteProductResult
			{
				Message= message
			};
		}
	}
}
