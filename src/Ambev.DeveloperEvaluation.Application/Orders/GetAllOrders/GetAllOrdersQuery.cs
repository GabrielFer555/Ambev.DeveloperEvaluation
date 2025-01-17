using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders
{
	public class GetAllOrdersQuery:IRequest<GetAllOrdersResult>
	{
		public int? _Page { get; set; }
		public int? _Limit { get; set; }
	}
}
