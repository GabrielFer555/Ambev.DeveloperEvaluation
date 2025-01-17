using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class OrderItem : Entity<int>
    {
        public int ProductId { get; set; }
        [JsonIgnore]
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; private set; } = 0;
        public int Quantity { get; set; }
        public OrderItemStatus OrderItemStatus { get; set; } = OrderItemStatus.Active;

		public decimal TotalPrice => Quantity * Price;
        public decimal TotalDiscount => GetTotalPriceWithDiscount();   

        public OrderItem()
        {
           
        }

        public OrderItem(int productId, decimal price, int quantity)
		{
			ProductId = productId;
			Price = price;
			Quantity = UpdateDiscountBasedOfQuantity(quantity);
		}

		public decimal GetTotalPriceWithDiscount()
        {
            if (Discount == 0) return TotalPrice;
            return TotalPrice - (TotalPrice * (Discount / 100m));
        }
        public int UpdateDiscountBasedOfQuantity(int quantity) { 
            if(quantity < 4) Discount = 0;
            if(quantity >= 4 && quantity < 10) Discount = 10;
            if(quantity >= 10 && quantity <= 20) Discount = 20;
            return quantity;
        }
    }
}
