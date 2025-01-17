using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder
{
	public class CancelOrderCommand:IRequest<CancelOrderResult>
	{
        public int Id { get; set; }
    }
}
