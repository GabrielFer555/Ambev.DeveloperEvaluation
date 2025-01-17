using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
	public class UpdateOrderHandler(IOrdersRepository repository, IMapper mapper) : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
	{
		public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateOrderCommandValidator();
			var isValid = await validator.ValidateAsync(request, cancellationToken);
			if (!isValid.IsValid) throw new ValidationException(isValid.Errors);

			var order = mapper.Map<Order>(request);
			var updatedOrder = await repository.UpdateOrder(order, cancellationToken);

			var result = mapper.Map<UpdateOrderResult>(updatedOrder);
			return result;

		}
	}
}
