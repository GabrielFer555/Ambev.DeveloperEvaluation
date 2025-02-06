namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder
{
    public class CreateOrderCommand:IRequest<CreateOrderResult>
    {
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<OrderItemCommandDto> Items { get; set; } = new();
    }
}
