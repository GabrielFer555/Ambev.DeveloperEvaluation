using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemCommandValidator:AbstractValidator<CancelOrderItemCommand>
	{
		public CancelOrderItemCommandValidator() {
			RuleFor(x => x.OrderItemId).NotEmpty();
			RuleFor(x => x.OrderId).NotEmpty();
		}
	}
}
