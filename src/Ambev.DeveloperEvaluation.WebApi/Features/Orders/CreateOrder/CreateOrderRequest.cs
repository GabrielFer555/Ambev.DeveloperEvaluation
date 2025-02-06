namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemCommandDto> Items { get; set; } = new();
        public string Branch { get; set; } = string.Empty;
    }
}
