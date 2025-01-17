using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder
{
	public class CancelOrderCommandValidator:AbstractValidator<CancelOrderCommand>
	{
		public CancelOrderCommandValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
