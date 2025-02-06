
namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
	public class Order:Aggregate<int>
	{
		public DateTime CreatedAt { get; set; }
		public Guid CustomerId { get; set; }
		public OrderStatus OrderStatus { get; set; } = OrderStatus.Active;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
		public string Branch = string.Empty;
		public decimal TotalPrice => Items.Where(x => x.OrderItemStatus != OrderItemStatus.Canceled)
			.Sum(x => x.TotalPrice);
		public decimal TotalAmountDiscount => Items.Where(x => x.OrderItemStatus != OrderItemStatus.Canceled)
			.Sum(x => x.GetTotalPriceWithDiscount());

		public Order() { }
	}
}
