using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders
{
	public class GetAllOrdersQueryValidator:AbstractValidator<GetAllOrdersQuery>
	{
		public GetAllOrdersQueryValidator() {
			RuleFor(x => x._Page).GreaterThanOrEqualTo(1);
			RuleFor(x => x._Limit).GreaterThanOrEqualTo(1);
		}
	}
}
