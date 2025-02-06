namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
	internal class DeleteCartHandler (ICartRepository repository) : IRequestHandler<DeleteCartCommand, DeleteCartResult>
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
