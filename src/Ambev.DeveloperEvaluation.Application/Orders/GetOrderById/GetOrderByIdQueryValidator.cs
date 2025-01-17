using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrderById
{
	public class GetOrderByIdQueryValidator:AbstractValidator<GetOrderByIdQuery>
	{
		public GetOrderByIdQueryValidator() {
			RuleFor(x => x.Id).NotEmpty().WithMessage("Order Id must be informed");
		}
	}
}
