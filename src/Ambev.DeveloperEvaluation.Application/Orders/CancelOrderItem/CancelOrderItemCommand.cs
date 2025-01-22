using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem
{
	public class CancelOrderItemCommand:IRequest<CancelOrderItemResult>
	{
        public int OrderId { get; set; }
		public int OrderItemId { get; set; }
	}
}
