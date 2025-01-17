using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderCommand:IRequest<CreateOrderResult>
    {
        public Guid CustomerId { get; set; }
        public string Branch { get; set; }
        public List<OrderItemCommandDto> Items { get; set; }
    }
}
