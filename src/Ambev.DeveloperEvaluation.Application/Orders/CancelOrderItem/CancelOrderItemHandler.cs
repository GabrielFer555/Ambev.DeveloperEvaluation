namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<CancelOrderItemCommand, CancelOrderItemResult>
	{
		public async Task<CancelOrderItemResult> Handle(CancelOrderItemCommand request, CancellationToken cancellationToken)
		{
			var validator = new CancelOrderItemCommandValidator();
			var isRequestValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isRequestValid.IsValid) throw new ValidationException(isRequestValid.Errors);

			var order = await ordersRepository.GetOrderByNumber(request.OrderId);
			var orderItem = order.Items.FirstOrDefault(x =>  x.OrderId == request.OrderId);
			if(orderItem is null) throw new NotFoundException("OrderItem", request.OrderItemId);
			var orderWithItemCancelled = await ordersRepository.CancelOrderItem(request.OrderId, request.OrderItemId);

			var itemsActive = orderWithItemCancelled.Items.FirstOrDefault(x => x.OrderItemStatus != OrderItemStatus.Canceled);
			if(itemsActive is null){
				await ordersRepository.CancelOrder(orderWithItemCancelled.Id, cancellationToken);
			}

            var result = mapper.Map<CancelOrderItemResult>(orderWithItemCancelled);

			return result;
		}
	}
}
