using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetAllOrders
{
	public class GetAllOrdersResult
	{
		public List<Order> Items { get; set; }
		public int Page { get; set; }
		public int Limit { get; set; }
		public int TotalPages { get; set; }
	}
}
