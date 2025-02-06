namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder
{
	public class CancelOrderHandler(IOrdersRepository repository) : IRequestHandler<CancelOrderCommand, CancelOrderResult>
	{
		public async Task<CancelOrderResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
		{
			var validator = new CancelOrderCommandValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			var result = await repository.CancelOrder(request.Id);
			if (!result) throw new BadRequestException("Error to cancel order");
			return new CancelOrderResult
			{
				Message="Order cancelled successfully"
			};
		}
	}
}
