using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder
{
	public class UpdateOrderCommand:IRequest<UpdateOrderResult>
	{
		public int Id { get; set; }
		public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public List<OrderItemCommandDto> Items { get; set; } = new();
	}
}
