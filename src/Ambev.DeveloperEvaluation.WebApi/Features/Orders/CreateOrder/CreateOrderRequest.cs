using Ambev.DeveloperEvaluation.Application.Orders;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemCommandDto> Items { get; set; }
        public string Branch { get; set; }
    }
}
