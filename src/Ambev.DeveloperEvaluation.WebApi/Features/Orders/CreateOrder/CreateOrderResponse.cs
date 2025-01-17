using Ambev.DeveloperEvaluation.Application.Orders;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderResponse
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
		public string Branch { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }
}
