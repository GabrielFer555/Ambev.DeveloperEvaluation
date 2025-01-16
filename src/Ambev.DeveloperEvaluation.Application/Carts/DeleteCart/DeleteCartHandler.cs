using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
	internal class DeleteCartHandler (IMapper mapper, ICartRepository repository) : IRequestHandler<DeleteCartCommand, DeleteCartResult>
	{
		public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
		{

			var validator = new DeleteCartValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) {
				throw new ValidationException(isValid.Errors);
			}

			var result = await repository.DeleteCart(request.Id, cancellationToken);
			if (!result) throw new BadRequestException("Error deleting cart");

			return new DeleteCartResult
			{
				Message = "Cart deleted successfully"
			};

		}
	}
}
